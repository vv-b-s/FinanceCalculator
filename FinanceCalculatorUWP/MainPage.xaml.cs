using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;

using Finance;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinanceCalculatorUWP
{
    public sealed partial class MainPage : Page
    {
        internal static int[] spinner = new int[2];
        internal static int spaces = 0;

        #region UI Control
        public MainPage()
        {
            this.InitializeComponent();

            ShowStatusBar();

            CSpinnerVisibility(false);
            InputBox.IsEnabled = false;
            CalculationButton.IsEnabled = false;

            #region OperationSpinner
            foreach (var item in Enum.GetValues(typeof(Calculate)))             // http://stackoverflow.com/questions/15040872/adding-enum-values-to-a-simple-combobox
                OperationSpinner.Items.Add(item);
            OperationSpinner.SelectedIndex = (int)Calculate.None;
            #endregion            
        }

        private static async void ShowStatusBar()
        {
            // turn on SystemTray for mobile
            // don't forget to add a Reference to Windows Mobile Extensions For The UWP
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusbar = StatusBar.GetForCurrentView();
                await statusbar.ShowAsync();
                statusbar.BackgroundColor = Windows.UI.Colors.Black;
                statusbar.BackgroundOpacity = 1;
                statusbar.ForegroundColor = Windows.UI.Colors.White;
            }
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataFlipper.Text = ModifyFlipper(InputBox.Text);
        }

        private void CalculationButton_Click(object sender, RoutedEventArgs e)
        {
            string[] attribute = InputBox.Text.Split();

            if (CheckInput(ref attribute))
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
                        ShowDialog("\aNotice:\nIf you have previously calculated something, enter 0 to directly use it.");
                        CSpinnerVisibility<Risk.CalcType>("Choose a risk operation:");

                        Risk.ExpectedReturns.ER.Clear();
                        Risk.StandardDeviation.sD.Clear();
                        Risk.PortfolioCovariation.PC.Clear();
                        Risk.CorelationCoefficient.CC.Clear();

                        break;
                    #endregion

                    #region Deprication
                    case (int)Calculate.Deprication:
                        CSpinnerVisibility<Deprication.DepricationType>("Choose deprication type:");
                        break;
                    #endregion

                    #region Annuity
                    case (int)Calculate.Annuity:
                        CSpinnerVisibility<Annuity.PresentOrFuture>("Present or future value?");
                        break;
                    #endregion

                    #region Stock and Bond price
                    case (int)Calculate.StockAndBondPrices:
                        CSpinnerVisibility<StockAndBondPrices.CalcType>("Choose what you want to calculate:");
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

        private void CSpinnerVisibility<TFormulaTypesEnum>(string labelText)                                   // Easily creates the data for the secons spinner
        {
            CalculationSpinner.Items.Clear();
            CSpinnerVisibility(true);

            foreach (var item in Enum.GetValues(typeof(TFormulaTypesEnum)))             // http://stackoverflow.com/questions/15040872/adding-enum-values-to-a-simple-combobox
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

        private static bool CheckInput(ref string[] input)
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

        private static T ExtractValue<T>(string input) where T : struct
        {
            var culture = new CultureInfo("bg-BG");

            if (typeof(T) == typeof(decimal))
            {
                decimal.TryParse(input, NumberStyles.Any, culture, out decimal output);    //http://stackoverflow.com/questions/11560465/parse-strings-to-double-with-comma-and-point
                return (T)(object)output;
            }
            else if (typeof(T) == typeof(int))
            {
                int.TryParse(input, NumberStyles.Any, culture, out int output);
                return (T)(object)output;
            }
            else if (typeof(T) == typeof(double))
            {
                double.TryParse(input, NumberStyles.Any, culture, out double output);
                return (T)(object)output;
            }
            return default(T);
        }

        #endregion


    }
}
