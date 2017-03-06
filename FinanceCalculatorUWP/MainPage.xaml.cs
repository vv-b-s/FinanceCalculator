using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Finance;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinanceCalculatorUWP
{
    public sealed partial class MainPage : Page
    {
        internal static int[] spinner = new int[2];
        internal static int spaces = 0;
        public MainPage()
        {
            this.InitializeComponent();
        }

        #region UI Control
        private void Layout_Loaded(object sender, RoutedEventArgs e)
        {
            CSpinnerVisibility(false);
            InputBox.IsEnabled = false;
            CalculationButton.IsEnabled = false;

            #region OperationSpinner
            foreach (var item in Enum.GetValues(typeof(Calculate)))             // http://stackoverflow.com/questions/15040872/adding-enum-values-to-a-simple-combobox
                OperationSpinner.Items.Add(item);
            OperationSpinner.SelectedIndex = (int)Calculate.None;
            #endregion            
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataFlipper.Text = ModifyFlipper(InputBox.Text);
        }

        private void CalculationButton_Click(object sender, RoutedEventArgs e)
        {
            string[] attribute = InputBox.Text.Split();

            if (CheckInput(ref attribute, (Calculate)spinner[0]))
                ResultBox.Text = DoCalculation(attribute);
            else ResultBox.Text = "Wrong input.";
        }

        private void OperationSpinner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearData();
            spinner[0] = OperationSpinner.SelectedIndex;

            #region Spinner 2 activation
            if (spinner[0] != (int)Calculate.None)
            {
                ClearData();
                InputBox.IsEnabled = true;
                DataFlipper.Text = ModifyFlipper(InputBox.Text);

                #region Second Spinner Condition
                switch (spinner[0])
                {
                    #region Future Value
                    case (int)Calculate.FutureValue:
                        CSpinnerVisibility<Interest.IntrestType>("Choose Interest type:");
                        break;
                    #endregion

                    #region Present Value
                    case (int)Calculate.PresentValue:
                        CSpinnerVisibility<Interest.IntrestType>("Choose Interest type:");
                        break;

                    #endregion

                    #region Effective Interest Rate
                    case (int)Calculate.EffectiveIR:
                        CSpinnerVisibility(false);
                        break;
                    #endregion

                    #region Rate of Return
                    case (int)Calculate.RateOfReturn:
                        CSpinnerVisibility(false);
                        break;
                    #endregion

                    #region Risk
                    case (int)Calculate.Risk:
                        CSpinnerVisibility<Risk.CalcType>("Choose a risk operation:");

                        Risk.ExpectedReturns.eR.Clear();
                        Risk.StandardDeviation.sD.Clear();
                        Risk.PortfolioCovariation.PC.Clear();
                        Risk.CorelationCoeficient.CC.Clear();

                        break;
                        #endregion
                }
                #endregion Second Spinner Condition
            }
            else
            {
                CSpinnerVisibility(false);
                InputBox.IsEnabled = false;
                CalculationButton.IsEnabled = false;
                DataFlipper.Text = "Enter data:";
            }
            #endregion
        }
        private void CalculationSpinner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spinner[1] = CalculationSpinner.SelectedIndex;
            ClearData();
            DataFlipper.Text = ModifyFlipper(InputBox.Text);
        }

        private void CSpinnerVisibility(bool state)
        {

            switch (state)
            {
                case true:
                    CSpinnerLabel.Visibility = Visibility.Visible;
                    CalculationSpinner.Visibility = Visibility.Visible;
                    break;
                case false:
                    CSpinnerLabel.Visibility = Visibility.Collapsed;
                    CalculationSpinner.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void CSpinnerVisibility<FormulaTypesEnum>(string labelText)                                   // Easily creates the data for the secons spinner
        {
            CalculationSpinner.Items.Clear();
            CSpinnerVisibility(true);

            foreach (var item in Enum.GetValues(typeof(FormulaTypesEnum)))             // http://stackoverflow.com/questions/15040872/adding-enum-values-to-a-simple-combobox
                CalculationSpinner.Items.Add(item);
            CalculationSpinner.SelectedIndex = 0;

            CSpinnerLabel.Text = labelText;
        }
        #endregion

        #region InputControl
        private void ClearData()
        {
            DataFlipper.Text = "Enter data:";
            InputBox.Text = "";
            ResultBox.Text = "";
            CalculationButton.IsEnabled = false;
            spaces = 0;
        }

        private bool CheckInput(ref string[] input, Calculate type)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == "")
                    return false;

                foreach (char a in input[i])
                    if ((a < '0' || a > '9') && a != ',' && a != '.' && a != ' ' && a != '-')
                        return false;

                input[i] = input[i].Replace('.', ',');           // Dots are used as decimal points too
            }
            return true;
        }

        private decimal ExtractValue(string input)
        {
            var culture = new CultureInfo("bg-BG");
            decimal output;
            decimal.TryParse(input, NumberStyles.Any,culture,out output);    //http://stackoverflow.com/questions/11560465/parse-strings-to-double-with-comma-and-point
            return output;
        }
        #endregion


    }
}
