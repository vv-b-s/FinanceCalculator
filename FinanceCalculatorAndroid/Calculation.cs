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

            #region Factors
            if (spinner[0] == (int)Calculate.Factors)
            {
                spaces = 0;
                CountSpaces(InputBox);
                CalculationButton.Enabled = spaces == 1;
                return FlipperFeeder(Factors.Attributes);
            }
            #endregion

            #region Interest
            else if (spinner[0] == (int)Calculate.FutureValue || spinner[0] == (int)Calculate.PresentValue)
            {
                spaces = 0;
                CountSpaces(InputBox);
                CalculationButton.Enabled = spaces == 2 || spaces == 4;
                return FlipperFeeder(spinner[0] == (int)Calculate.FutureValue ? Interest.FutureValue.Attributes : Interest.PresentValue.Attributes);
            }
            #endregion

            #region Effective Interest Rate
            if (spinner[0] == (int)Calculate.EffectiveIR)
            {
                spaces = 0;
                CountSpaces(InputBox);
                CalculationButton.Enabled = spaces == 2;
                return FlipperFeeder(Interest.EffectiveIR.Attributes);
            }

            #endregion

            #region Rate of Return
            if (spinner[0] == (int)Calculate.RateOfReturn)
            {
                spaces = 0;
                CountSpaces(InputBox);
                CalculationButton.Enabled = spaces == 1;
                return FlipperFeeder(RateOfReturn.Attributes);
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
                        CalculationButton.Enabled = spaces == 1;
                        return FlipperFeeder(Risk.ExpectedReturns.Attributes);

                    case (int)Risk.CalcType.StandardDeviation:
                        CalculationButton.Enabled = spaces == 2;
                        return FlipperFeeder(Risk.StandardDeviation.Attributes);

                    case (int)Risk.CalcType.VariationCoefficient:
                        CalculationButton.Enabled = spaces == 1;
                        return FlipperFeeder(Risk.VariationCoefficient.Attributes);

                    case (int)Risk.CalcType.PortfolioCovariation:
                        CalculationButton.Enabled = spaces == 4;
                        return FlipperFeeder(Risk.PortfolioCovariation.Attributes);

                    case (int)Risk.CalcType.CorelationCoefficient:
                        CalculationButton.Enabled = spaces == 2;
                        return FlipperFeeder(Risk.CorelationCoefficient.Attributes);

                    case (int)Risk.CalcType.PortfolioDeviation:
                        CalculationButton.Enabled = spaces == 4;
                        return FlipperFeeder(Risk.PortfolioDeviation.Attributes);

                    case (int)Risk.CalcType.BetaCoefficient:
                        CalculationButton.Enabled = spaces == 1;
                        return FlipperFeeder(Risk.BetaCoefficient.Attributes);
                }
            }
            #endregion

            #region Deprication
            if (spinner[0] == (int)Calculate.Deprication)
            {
                spaces = 0;
                CountSpaces(InputBox);

                switch (spinner[1])
                {
                    case (int)Deprication.DepricationType.Linear:
                        CalculationButton.Enabled = spaces == 2 || spaces == 3;
                        return FlipperFeeder(Deprication.LinearDeprication.Attributes);

                    case (int)Deprication.DepricationType.DecreasingDeduction:
                        CalculationButton.Enabled = spaces == 3;
                        return FlipperFeeder(Deprication.DecreasingDeduction.Attributes);

                    case (int)Deprication.DepricationType.ComulativeMethod:
                        CalculationButton.Enabled = spaces == 2;
                        return FlipperFeeder(Deprication.ComulativeMethod.Attributes);

                    case (int)Deprication.DepricationType.EqualDegression:
                        CalculationButton.Enabled = spaces == 2;
                        return FlipperFeeder(Deprication.EqualDegression.Attributes);
                }
            }
            #endregion

            #region Annuity
            else if (spinner[0] == (int)Calculate.Annuity)
            {
                spaces = 0;
                CountSpaces(InputBox);
                CalculationButton.Enabled = spaces == 3 || spaces == 5;
                return FlipperFeeder(spinner[1] == (int)Annuity.PresentOrFuture.Future ? Annuity.FutureValue.Attributes : Annuity.PresentValue.Attributes);
            }
            #endregion

            #region Stock and Bond price
            else if (spinner[0] == (int)Calculate.StockAndBondPrices)
            {
                spaces = 0;
                CountSpaces(InputBox);
                switch (spinner[1])
                {
                    case (int)StockAndBondPrices.CalcType.BondYield:
                        CalculationButton.Enabled = spaces == 2;
                        return FlipperFeeder(StockAndBondPrices.BondYield.Attributes);

                    case (int)StockAndBondPrices.CalcType.PresentDiscountBondPrice:
                        CalculationButton.Enabled = spaces == 3;
                        return FlipperFeeder(StockAndBondPrices.PresentDiscountBondPrice.Attributes);

                    case (int)StockAndBondPrices.CalcType.CuponBondPrice:
                        CalculationButton.Enabled = spaces == 3;
                        return FlipperFeeder(StockAndBondPrices.CuponBondPrice.Attributes);

                    case (int)StockAndBondPrices.CalcType.PerpetuityPrice:
                        CalculationButton.Enabled = spaces >= 1 && spaces <= 2;
                        return FlipperFeeder(StockAndBondPrices.PerpetuityPrice.Attributes);

                    case (int)StockAndBondPrices.CalcType.PreferredStockPrice:
                        CalculationButton.Enabled = spaces == 1;
                        return FlipperFeeder(StockAndBondPrices.PreferredStockPrice.Attributes);

                    case (int)StockAndBondPrices.CalcType.CommonSharePrice:
                        CalculationButton.Enabled = spaces >= 2 && spaces <= 3;
                        return FlipperFeeder(StockAndBondPrices.CommonSharePrice.Attributes);

                    case (int)StockAndBondPrices.CalcType.RateOfIncreasing:
                        CalculationButton.Enabled = spaces == 2;
                        return FlipperFeeder(StockAndBondPrices.RateOfIncreasing.Attributes);
                }
            }
            #endregion

            #region Asset Investment
            else if (spinner[0] == (int)Calculate.AssetInvestment)
            {
                spaces = 0;
                CountSpaces(InputBox);
                switch (spinner[1])
                {
                    case (int)AssetInvestment.AssetValues.NetCashFlows:
                        CalculationButton.Enabled = (spaces == 4);
                        return FlipperFeeder(AssetInvestment.NetCashFlows.Attributes);

                    case (int)AssetInvestment.AssetValues.AverageIncomeNorm:
                        int neededSpaces          =     (spaces < 2)                            ? 3 : 1 + ExtractValue <int> (InputBox.Split()[1]);
                        CalculationButton.Enabled =     (spaces == neededSpaces);
                        return                          (spaces >= 2 && spaces <= neededSpaces) ? AssetInvestment.AverageIncomeNorm.Attributes[2] :
                            FlipperFeeder(AssetInvestment.AverageIncomeNorm.Attributes);

                    case (int)AssetInvestment.AssetValues.NetPresentValue:
                        neededSpaces              =     (spaces < 3)                            ? 4 : 2 + ExtractValue <int> (InputBox.Split()[2]);
                        CalculationButton.Enabled =     (spaces == neededSpaces);
                        return                          (spaces >= 3 && spaces <= neededSpaces) ? AssetInvestment.NetPresentValue.Attributes[3] :
                            FlipperFeeder(AssetInvestment.NetPresentValue.Attributes);

                    case (int)AssetInvestment.AssetValues.ProfitabilityIndex:
                        neededSpaces              = (spaces < 3)                                ? 4 : 2 + ExtractValue <int> (InputBox.Split()[2]);
                        CalculationButton.Enabled = (spaces == neededSpaces);
                        return                      (spaces >= 3 && spaces <= neededSpaces)     ? AssetInvestment.ProfitabilityIndex.Attributes[3] :
                            FlipperFeeder(AssetInvestment.ProfitabilityIndex.Attributes);

                    case (int)AssetInvestment.AssetValues.PaybackPeriod:
                        neededSpaces              = (spaces < 3)                                ? 4 : 2 + ExtractValue<int>(InputBox.Split()[2]);
                        CalculationButton.Enabled = (spaces == neededSpaces);
                        return                      (spaces >= 3 && spaces <= neededSpaces)     ? AssetInvestment.PaybackPeriod.Attributes[3] :
                            FlipperFeeder(AssetInvestment.PaybackPeriod.Attributes);
                }
            }
            #endregion
            return "";
        }

        private string FlipperFeeder(string[] Attributes) => (spaces <= Attributes.Length - 1)  ? $"Enter: {Attributes[spaces]}" :
            "There is no more data to be filled.";

        private string DoCalculation(string[] attribute)
        {
            #region Factors
            if (spinner[0] == (int)Calculate.Factors)
                return Factors.ShowFactors (ExtractValue <int>    (attribute[0]),
                                            ExtractValue <double> (attribute[1]));
            #endregion

            #region Future Value
            else if (spinner[0] == (int)Calculate.FutureValue)
            {
                switch (spinner[1])
                {
                    case (int)Interest.IntrestType.Simple:
                        return Interest.FutureValue.SimpleInterest (ExtractValue <decimal> (attribute[0]),
                                                                    ExtractValue <decimal> (attribute[1]),
                                                                    ExtractValue <double>  (attribute[2]));

                    case (int)Interest.IntrestType.Discursive:
                        if      (spaces == 2)
                            return Interest.FutureValue.CDiscursiveInterest(ExtractValue <decimal> (attribute[0]),
                                                                            ExtractValue <decimal> (attribute[1]),
                                                                            ExtractValue <double>  (attribute[2]));
                        else if (spaces == 4)
                            return Interest.FutureValue.CDiscursiveInterest(ExtractValue <decimal> (attribute[0]),
                                                                            ExtractValue <decimal> (attribute[1]),
                                                                            ExtractValue <double>  (attribute[2]),
                                                                            ExtractValue <double>  (attribute[3]),
                                                 (Interest.InterestPeriods) ExtractValue <int>     (attribute[4]));
                        break;

                    case (int)Interest.IntrestType.Anticipative:
                        if (spaces == 2)
                            return Interest.FutureValue.CAnticipativeInterest(ExtractValue <decimal> (attribute[0]), 
                                                                              ExtractValue <decimal> (attribute[1]), 
                                                                              ExtractValue <double>  (attribute[2]));
                        else if (spaces == 4)
                            return Interest.FutureValue.CAnticipativeInterest(ExtractValue <decimal> (attribute[0]),
                                                                              ExtractValue <decimal> (attribute[1]),
                                                                              ExtractValue <double>  (attribute[2]),
                                                                              ExtractValue <double>  (attribute[3]),
                                                   (Interest.InterestPeriods) ExtractValue <int>     (attribute[4]));
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
                        return Interest.PresentValue.SimpleInterest(ExtractValue <decimal> (attribute[0]),
                                                                    ExtractValue <decimal> (attribute[1]),
                                                                    ExtractValue <double>  (attribute[2]));

                    case (int)Interest.IntrestType.Discursive:
                        if (spaces == 2)
                            return Interest.PresentValue.CDiscursiveInterest(ExtractValue <decimal> (attribute[0]),
                                                                             ExtractValue <decimal> (attribute[1]),
                                                                             ExtractValue <double>  (attribute[2]));
                        else if (spaces == 4)
                            return Interest.PresentValue.CDiscursiveInterest(ExtractValue <decimal> (attribute[0]),
                                                                             ExtractValue <decimal> (attribute[1]), 
                                                                             ExtractValue <double>  (attribute[2]), 
                                                                             ExtractValue <double>  (attribute[3]),
                                                  (Interest.InterestPeriods) ExtractValue <int>     (attribute[4]));
                        break;

                    case (int)Interest.IntrestType.Anticipative:
                        if (spaces == 2)
                            return Interest.PresentValue.CAnticipativeInterest(ExtractValue <decimal> (attribute[0]),
                                                                               ExtractValue <decimal> (attribute[1]), 
                                                                               ExtractValue <double>  (attribute[2]));
                        else if (spaces == 4)
                            return Interest.PresentValue.CAnticipativeInterest(ExtractValue <decimal> (attribute[0]), 
                                                                               ExtractValue <decimal> (attribute[1]),
                                                                               ExtractValue <double>  (attribute[2]), 
                                                                               ExtractValue <double>  (attribute[3]), 
                                                    (Interest.InterestPeriods) ExtractValue <int>     (attribute[4]));
                        break;
                }
            }
            #endregion

            #region Effective Interest Rate
            else if (spinner[0] == (int)Calculate.EffectiveIR)
                return Interest.EffectiveIR.Calculate(ExtractValue <decimal> (attribute[0]),
                                                      ExtractValue <double>  (attribute[1]),
                           (Interest.InterestPeriods) ExtractValue <int>     (attribute[2]));
            #endregion

            #region Rate of Return
            else if (spinner[0] == (int)Calculate.RateOfReturn)
                return RateOfReturn.Calculate(ExtractValue <decimal> (attribute[0]),
                                              ExtractValue <decimal> (attribute[1]));
            #endregion

            #region Risk
            else if (spinner[0] == (int)Calculate.Risk)
            {
                switch (spinner[1])
                {
                    case (int)Risk.CalcType.ExpectedReturns:
                        return Risk.ExpectedReturns.ER.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                 ExtractValue <decimal> (attribute[1]));

                    case (int)Risk.CalcType.StandardDeviation:
                        return Risk.StandardDeviation.sD.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                   ExtractValue <decimal> (attribute[1]), 
                        (attribute[2] == "0") ?                    Risk.ExpectedReturns.ER.Value : 
                                                                   ExtractValue <decimal> (attribute[2]));           // if Expected Returns is equal to 0, the app will use Risk.ExpectedReturns.ER's data

                    case (int)Risk.CalcType.VariationCoefficient:
                        return Risk.VariationCoefficient.Calculate((attribute[0] == "0") ? 
                                                                    Risk.StandardDeviation.sD.Value : 
                                                                    ExtractValue <decimal> (attribute[0]),
                                            (attribute[1] == "0") ? Risk.ExpectedReturns.ER.Value : 
                                                                    ExtractValue <decimal> (attribute[1]));

                    case (int)Risk.CalcType.PortfolioCovariation:
                        return Risk.PortfolioCovariation.PC.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                      ExtractValue <decimal> (attribute[1]),
                                                                      ExtractValue <decimal> (attribute[2]),
                                                                      ExtractValue <decimal> (attribute[3]), 
                                                                      ExtractValue <decimal> (attribute[4]));

                    case (int)Risk.CalcType.CorelationCoefficient:
                        return Risk.CorelationCoefficient.CC.Calculate((attribute[0] == "0") ? 
                                                                        Risk.PortfolioCovariation.PC.Value :
                                                                        ExtractValue <decimal> (attribute[0]),
                                                                        ExtractValue <decimal> (attribute[1]),
                                                                        ExtractValue <decimal> (attribute[2]));

                    case (int)Risk.CalcType.PortfolioDeviation:
                        return Risk.PortfolioDeviation.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                 ExtractValue <decimal> (attribute[1]),
                                                                 ExtractValue <decimal> (attribute[2]), 
                                                                 ExtractValue <decimal> (attribute[3]),
                                         (attribute[4] == "0") ? Risk.CorelationCoefficient.CC.Value :
                                                                 ExtractValue <decimal> (attribute[4]));

                    case (int)Risk.CalcType.BetaCoefficient:
                        return Risk.BetaCoefficient.Calculate((attribute[0] == "0") ? 
                                                               Risk.PortfolioCovariation.PC.Value :
                                                               ExtractValue <decimal> (attribute[0]),
                                      (attribute[1] == "0") ? (decimal)Math.Pow((double)Risk.StandardDeviation.sD.Value, 2) :
                                                               ExtractValue <decimal> (attribute[1]));
                }
            }
            #endregion

            #region Deprication
            else if (spinner[0] == (int)Calculate.Deprication)
                switch (spinner[1])
                {
                    case (int)Deprication.DepricationType.Linear:
                        if (spaces == 2)
                            return Deprication.LinearDeprication.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                           ExtractValue <decimal> (attribute[1]),
                                                                           ExtractValue <int>     (attribute[2]));
                        if (spaces == 3)
                            return Deprication.LinearDeprication.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                           ExtractValue <decimal> (attribute[1]),
                                                                           ExtractValue <int>     (attribute[2]),
                                                                           ExtractValue <decimal> (attribute[3]));
                        break;

                    case (int)Deprication.DepricationType.DecreasingDeduction:
                        return Deprication.DecreasingDeduction.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                         ExtractValue <decimal> (attribute[1]),
                                                                         ExtractValue <int>     (attribute[2]),
                                                                         ExtractValue <decimal> (attribute[3]));

                    case (int)Deprication.DepricationType.ComulativeMethod:
                        return Deprication.ComulativeMethod.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                      ExtractValue <decimal> (attribute[1]),
                                                                      ExtractValue <int>     (attribute[2]));

                    case (int)Deprication.DepricationType.EqualDegression:
                        return Deprication.ComulativeMethod.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                      ExtractValue <decimal> (attribute[1]),
                                                                      ExtractValue <int>     (attribute[2]));
                }
            #endregion

            #region Annuity
            else if (spinner[0] == (int)Calculate.Annuity)
            {
                switch (spinner[1])
                {
                    case (int)Annuity.PresentOrFuture.Future:
                        return spaces == 3 ?
                            Annuity.FutureValue.Calculate(ExtractValue<int>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]), ExtractValue<double>(attribute[3])) :
                            Annuity.FutureValue.Calculate(ExtractValue<int>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]), ExtractValue<double>(attribute[3]), ExtractValue<double>(attribute[4]), (Interest.InterestPeriods)ExtractValue<int>(attribute[5]));
                    case (int)Annuity.PresentOrFuture.Present:
                        return spaces == 3 ?
                            Annuity.PresentValue.Calculate(ExtractValue<int>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]), ExtractValue<double>(attribute[3])) :
                            Annuity.PresentValue.Calculate(ExtractValue<int>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]), ExtractValue<double>(attribute[3]), ExtractValue<double>(attribute[4]), (Interest.InterestPeriods)ExtractValue<int>(attribute[5]));
                } 
            }
            #endregion

            #region Stock and Bond price
            else if (spinner[0] == (int)Calculate.StockAndBondPrices)
            {
                switch (spinner[1])
                {
                    case (int)StockAndBondPrices.CalcType.BondYield:
                        return attribute[2] == "1" ?
                            StockAndBondPrices.BondYield.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1])) :
                            StockAndBondPrices.BondYield.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<int>(attribute[2]));

                    case (int)StockAndBondPrices.CalcType.PresentDiscountBondPrice:
                        return StockAndBondPrices.PresentDiscountBondPrice.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]), ExtractValue<int>(attribute[3]));

                    case (int)StockAndBondPrices.CalcType.CuponBondPrice:
                        return StockAndBondPrices.CuponBondPrice.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]), ExtractValue<int>(attribute[3]));

                    case (int)StockAndBondPrices.CalcType.PerpetuityPrice:
                        return StockAndBondPrices.PerpetuityPrice.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1]), attribute.Length < 3 ? 0 : ExtractValue<decimal>(attribute[2]));

                    case (int)StockAndBondPrices.CalcType.PreferredStockPrice:
                        return StockAndBondPrices.PreferredStockPrice.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1]));

                    case (int)StockAndBondPrices.CalcType.CommonSharePrice:
                        return StockAndBondPrices.CommonSharePrice.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]), attribute.Length == 4 ? ExtractValue<int>(attribute[3]) : 0);

                    case (int)StockAndBondPrices.CalcType.RateOfIncreasing:
                        return StockAndBondPrices.RateOfIncreasing.Calculate(ExtractValue<decimal>(attribute[0]), ExtractValue<decimal>(attribute[1]), ExtractValue<decimal>(attribute[2]));
                }
            }
            #endregion

            #region Asset Investment
            else if (spinner[0] == (int)Calculate.AssetInvestment)
            {
                switch (spinner[1])
                {
                    case (int)AssetInvestment.AssetValues.NetCashFlows:
                        return AssetInvestment.NetCashFlows.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                      ExtractValue <decimal> (attribute[1]),
                                                                      ExtractValue <decimal> (attribute[2]),
                                                                      ExtractValue <decimal> (attribute[3]), 
                                                                      ExtractValue <decimal> (attribute[4]));

                    case (int)AssetInvestment.AssetValues.AverageIncomeNorm:
                        decimal[] netIncomeEA = new decimal[ExtractValue <int> (attribute[1])];

                        for (int i = 0; i < ExtractValue <int> (attribute[1]); i++)
                            netIncomeEA[i]    = ExtractValue <decimal> (attribute[i + 2]);

                        return AssetInvestment.AverageIncomeNorm.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                           ExtractValue <int>     (attribute[1]),
                                                                           netIncomeEA);

                    case (int)AssetInvestment.AssetValues.NetPresentValue:
                        decimal[] cashFlowsEA = new decimal[ExtractValue <int> (attribute[2])];

                        for (int i = 0; i < ExtractValue <int> (attribute[2]); i++)
                            cashFlowsEA[i]   = ExtractValue <decimal> (attribute[i + 3]);

                        return AssetInvestment.NetPresentValue.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                         ExtractValue <decimal> (attribute[1]),
                                                                         ExtractValue <int>     (attribute[2]),
                                                                         cashFlowsEA);

                    case (int)AssetInvestment.AssetValues.ProfitabilityIndex:
                        cashFlowsEA = new decimal[ExtractValue <int> (attribute[2])];

                        for (int i = 0; i < ExtractValue <int> (attribute[2]); i++)
                            cashFlowsEA[i] = ExtractValue <decimal> (attribute[i + 3]);
                        return AssetInvestment.ProfitabilityIndex.Calculate(ExtractValue <decimal> (attribute[0]),
                                                                            ExtractValue <decimal> (attribute[1]),
                                                                            ExtractValue <int>     (attribute[2]),
                                                                            cashFlowsEA);

                    case (int)AssetInvestment.AssetValues.PaybackPeriod:
                        cashFlowsEA = new decimal[ExtractValue<int>(attribute[2])];

                        for (int i = 0; i < ExtractValue<int>(attribute[2]); i++)
                            cashFlowsEA[i] = ExtractValue<decimal>(attribute[i + 3]);
                        return AssetInvestment.PaybackPeriod.Calculate(ExtractValue<decimal>(attribute[0]),
                                                                            ExtractValue<decimal>(attribute[1]),
                                                                            ExtractValue<int>(attribute[2]),
                                                                            cashFlowsEA);
                }
            }
            #endregion
            return "";
        }

        private void ShowDialog(string text)             // When 0 is entered in some formulas, DoCalculation uses stored values depending on the argument required
        {
            var msgPop = new AlertDialog.Builder(this);
            msgPop.SetMessage(text);
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