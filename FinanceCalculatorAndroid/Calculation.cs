using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Finance;

namespace FinanceCalculator
{
    public partial class MainActivity : Activity
    {
        private string ModifyFlipper(string InputBox)
        {
            var CalculationButton = FindViewById<Button>(Resource.Id.CalculationButton);

            #region Future Value
            if (spinner[0] == (int)Calculate.FutureValue)
            {
                spaces = 0;
                CountSpaces(InputBox);

                if (spaces != 2 && spaces != 4)
                    CalculationButton.Enabled = false;
                else
                    CalculationButton.Enabled = true;

                return FlipperFeeder(Interest.FutureValue.attributes);
            }
            #endregion

            #region Present Value
            else if (spinner[0] == (int)Calculate.PresentValue)
            {
                spaces = 0;
                CountSpaces(InputBox);

                if (spaces != 2 && spaces != 4)
                    CalculationButton.Enabled = false;
                else
                    CalculationButton.Enabled = true;

                return FlipperFeeder(Interest.PresentValue.attributes);
            }
            #endregion

            #region Effective Interest Rate
            if (spinner[0] == (int)Calculate.EffectiveIR)
            {
                spaces = 0;
                CountSpaces(InputBox);

                if (spaces == 2)
                    CalculationButton.Enabled = true;
                else
                    CalculationButton.Enabled = false;

                return FlipperFeeder(Interest.EffectiveIR.attributes);
            }

            #endregion

            #region Rate of Return
            if (spinner[0] == (int)Calculate.RateOfReturn)
            {

                spaces = 0;
                CountSpaces(InputBox);

                if (spaces == 1)
                    CalculationButton.Enabled = true;
                else CalculationButton.Enabled = false;

                return FlipperFeeder(RateOfReturn.attributes);
            }

            #endregion

            #region Risk
            if (spinner[0] == (int)Calculate.Risk)
            {
                spaces = 0;
                CountSpaces(InputBox);

                switch (spinner[1])
                {
                    case (int)Risk.CalcType.ExpectedReturns:
                        if (spaces == 1)
                            CalculationButton.Enabled = true;
                        else CalculationButton.Enabled = false;
                        return FlipperFeeder(Risk.ExpectedReturns.attributes);

                    case (int)Risk.CalcType.StandardDeviation:
                        if (spaces == 2)
                            CalculationButton.Enabled = true;
                        else CalculationButton.Enabled = false;
                        return FlipperFeeder(Risk.StandardDeviation.attributes);

                    case (int)Risk.CalcType.VariationCoefficient:
                        if (spaces == 1)
                            CalculationButton.Enabled = true;
                        else CalculationButton.Enabled = false;
                        return FlipperFeeder(Risk.VariationCoefficient.attributes);

                    case (int)Risk.CalcType.PortfolioCovariation:
                        if (spaces == 4)
                            CalculationButton.Enabled = true;
                        else CalculationButton.Enabled = false;
                        return FlipperFeeder(Risk.PortfolioCovariation.attributes);

                    case (int)Risk.CalcType.CorelationCoeficient:
                        if (spaces == 2)
                            CalculationButton.Enabled = true;
                        else CalculationButton.Enabled = false;
                        return FlipperFeeder(Risk.CorelationCoeficient.attributes);

                    case (int)Risk.CalcType.PortfolioDeviation:
                        if (spaces == 4)
                            CalculationButton.Enabled = true;
                        else CalculationButton.Enabled = false;
                        return FlipperFeeder(Risk.PortfolioDeviation.attributes);

                    case (int)Risk.CalcType.BetaCoeficient:
                        if (spaces == 1)
                            CalculationButton.Enabled = true;
                        else CalculationButton.Enabled = false;
                        return FlipperFeeder(Risk.BetaCoeficient.attributes);
                }
            }
            #endregion


            return "";
        }

        private string FlipperFeeder(string[] attributes) => (spaces <= attributes.Length - 1) ? $"Enter: {attributes[spaces]}" : "There is no more data to be filled.";

        private string DoCalculation(string[] attribute)
        {
            #region Future Value
            if (spinner[0] == (int)Calculate.FutureValue)
            {
                switch (spinner[1])
                {
                    case (int)Interest.IntrestType.Simple:
                        return Interest.FutureValue.SimpleInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]));

                    case (int)Interest.IntrestType.Discursive:
                        if (spaces == 2)
                            return Interest.FutureValue.CDiscursiveInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]));
                        else if (spaces == 4)
                            return Interest.FutureValue.CDiscursiveInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]), (double)ExtractValue(attribute[3]), (Interest.InterestPeriods)(int)ExtractValue(attribute[4]));
                        break;

                    case (int)Interest.IntrestType.Anticipative:
                        if (spaces == 2)
                            return Interest.FutureValue.CAnticipativeInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]));
                        else if (spaces == 4)
                            return Interest.FutureValue.CAnticipativeInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]), (double)ExtractValue(attribute[3]), (Interest.InterestPeriods)(int)ExtractValue(attribute[4]));
                        break;
                }
            }
            #endregion

            #region Present Value
            else if (spinner[0] == (int)Calculate.PresentValue)
            {
                switch (spinner[1])
                {
                    case (int)Interest.IntrestType.Simple:
                        return Interest.PresentValue.SimpleInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]));

                    case (int)Interest.IntrestType.Discursive:
                        if (spaces == 2)
                            return Interest.PresentValue.CDiscursiveInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]));
                        else if (spaces == 4)
                            return Interest.PresentValue.CDiscursiveInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]), (double)ExtractValue(attribute[3]), (Interest.InterestPeriods)(int)ExtractValue(attribute[4]));
                        break;

                    case (int)Interest.IntrestType.Anticipative:
                        if (spaces == 2)
                            return Interest.PresentValue.CAnticipativeInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]));
                        else if (spaces == 4)
                            return Interest.PresentValue.CAnticipativeInterest(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (double)ExtractValue(attribute[2]), (double)ExtractValue(attribute[3]), (Interest.InterestPeriods)(int)ExtractValue(attribute[4]));
                        break;
                }
            }
            #endregion

            #region Effective Interest Rate
            else if (spinner[0] == (int)Calculate.EffectiveIR)
                return Interest.EffectiveIR.Calculate(ExtractValue(attribute[0]), (double)ExtractValue(attribute[1]), (Interest.InterestPeriods)((int)ExtractValue(attribute[2])));
            #endregion

            #region Rate of Return
            else if (spinner[0] == (int)Calculate.RateOfReturn)
                return RateOfReturn.Calculate(ExtractValue(attribute[0]), ExtractValue(attribute[1]));
            #endregion

            #region Risk
            else if (spinner[0] == (int)Calculate.Risk)
            {
                switch (spinner[1])
                {
                    case (int)Risk.CalcType.ExpectedReturns:
                        return Risk.ExpectedReturns.eR.Calculate(ExtractValue(attribute[0]), ExtractValue(attribute[1]));

                    case (int)Risk.CalcType.StandardDeviation:
                        return Risk.StandardDeviation.sD.Calculate(ExtractValue(attribute[0]), ExtractValue(attribute[1]), (attribute[2] == "0") ? Risk.ExpectedReturns.eR.Value : ExtractValue(attribute[2]));           // if Expected Returns is equal to 0, the app will use Risk.ExpectedReturns.eR's data

                    case (int)Risk.CalcType.VariationCoefficient:
                        return Risk.VariationCoefficient.Calculate((attribute[0] == "0") ? Risk.StandardDeviation.sD.Value : ExtractValue(attribute[0]), (attribute[1] == "0") ? Risk.ExpectedReturns.eR.Value : ExtractValue(attribute[1]));

                    case (int)Risk.CalcType.PortfolioCovariation:
                        return Risk.PortfolioCovariation.PC.Calculate(ExtractValue(attribute[0]), ExtractValue(attribute[1]), ExtractValue(attribute[2]), ExtractValue(attribute[3]), ExtractValue(attribute[4]));

                    case (int)Risk.CalcType.CorelationCoeficient:
                        return Risk.CorelationCoeficient.CC.Calculate((attribute[0] == "0") ? Risk.PortfolioCovariation.PC.Value : ExtractValue(attribute[0]), ExtractValue(attribute[1]), ExtractValue(attribute[2]));

                    case (int)Risk.CalcType.PortfolioDeviation:
                        return Risk.PortfolioDeviation.Calculate(ExtractValue(attribute[0]), ExtractValue(attribute[1]), ExtractValue(attribute[2]), ExtractValue(attribute[3]), (attribute[4] == "0") ? Risk.CorelationCoeficient.CC.Value : ExtractValue(attribute[4]));

                    case (int)Risk.CalcType.BetaCoeficient:
                        return Risk.BetaCoeficient.Calculate((attribute[0] == "0") ? Risk.PortfolioCovariation.PC.Value : ExtractValue(attribute[0]), (attribute[1] == "0") ? (decimal)Math.Pow((double)Risk.StandardDeviation.sD.Value, 2) : ExtractValue(attribute[1]));
                }
            }
            #endregion

            return "";
        }

        private void ZeroAlert()                    // When 0 is entered in some formulas, DoCalculation uses stored values depending on the argument required
        {
            var msgPop = new AlertDialog.Builder(this);
            msgPop.SetMessage("Notice:\nIf you have previously calculated something, enter 0 to directly use it.");
            msgPop.SetNeutralButton("Ok", delegate { });

            msgPop.Show();
        }
        private void CountSpaces(string text)
        {
            foreach (char a in text)               // measuring spaces in order to define which attribute to display
                if (a == ' ')
                    spaces++;
        }

    }
}