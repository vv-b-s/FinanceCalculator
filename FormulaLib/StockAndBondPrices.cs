using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace Finance
{
    public static class StockAndBondPrices
    {
        public enum CalcType { BondYield, PresentDiscountBondPrice, CuponBondPrice, PerpetuityPrice, PreferredStockPrice, CommonSharePrice, RateOfIncreasing }

        #region Bonds
        public static class BondYield
        {
            public static readonly string[] Attributes = { "Nominal Value", "Acquisition price", "Holding period" };

            public static string Calculate(decimal nominalValue, decimal acqPrice)
            {
                try
                {
                    decimal BMY = Round((nominalValue - acqPrice) / acqPrice, 2) * 100;

                    return $"Bond Maturity Yield: {BMY}%\n" +
                        $"Used formula: r = (N-Po)/Po\n" +
                        $"Solution: ({nominalValue} - {acqPrice})/{acqPrice} = {BMY / 100}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            public static string Calculate(decimal nominalValue, decimal acqPrice, int holdingPeriod)
            {
                try
                {
                    decimal BMY = (decimal)Pow((double)(nominalValue / acqPrice), 1d / holdingPeriod);
                    BMY = Round(BMY, 2) * 100;

                    return $"Bond Maturity Yield: {BMY}%\n" +
                        $"Used formula: r = Root[n](N/Po)\n" +
                        $"Solution: Root[{holdingPeriod}]({nominalValue}/{acqPrice}) = {BMY / 100}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        public static class PresentDiscountBondPrice
        {
            public static readonly string[] Attributes = { "Nominal value", "Selling price", "Rate of return", "Holding period" };

            public static string Calculate(decimal nominalValue, decimal sellingPrice, decimal RoR, int holdingPeriod)
            {
                try
                {
                    RoR /= 100;

                    decimal presentValue = nominalValue / (decimal)Pow((double)(1 + RoR), holdingPeriod);
                    presentValue = Round(presentValue, 2);

                    return $"Present value: {presentValue}\n" +
                        $"Net present value: {presentValue} - {sellingPrice} = {presentValue - sellingPrice}\n" +
                        $"Used Formula: Po = N/(1+r)^n\n" +
                        $"Solution: {nominalValue}/(1 + {RoR})^{holdingPeriod}) = {presentValue}" +
                        $"\n\nThe bond {(presentValue - sellingPrice > 0 ? "can be acquired!" : "shouldn't be acqired!")}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        public static class CuponBondPrice
        {
            public static readonly string[] Attributes = { "Nominal value", "Interest rate", "Discount rate", "Holding Period" };

            public static string Calculate(decimal nominalValue, decimal interestRate, decimal discountRate, int holdingPeriod)
            {
                try
                {
                    interestRate /= 100;
                    discountRate /= 100;

                    decimal CBP = 0;
                    var sB = new StringBuilder();

                    decimal annualYearInt = nominalValue * interestRate;

                    for (int n = 1; n <= holdingPeriod - 1; n++)
                    {
                        CBP += annualYearInt / (decimal)Pow((double)(1 + discountRate), n);
                        sB.Append($"{annualYearInt}/(1 + {discountRate})^{n} + ");
                    }

                    CBP += (annualYearInt + nominalValue) / (decimal)Pow((double)(1 + discountRate), holdingPeriod);
                    CBP = Round(CBP, 2);

                    sB.Append($"({annualYearInt}+{nominalValue})/(1 + {discountRate})^{holdingPeriod} = {CBP}");

                    return $"Cupon bound price: {CBP}\n" +
                        $"Annual year interest: {nominalValue} × {interestRate} = {annualYearInt}\n" +
                        $"Used formula: L/(1+r)^1+L/(1+r)^2+...+L/(1+r)^n\n" +
                        $"Solution: {sB.ToString()}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        #endregion

        public static class PerpetuityPrice
        {
            public static readonly string[] Attributes = { "Recurring payments", "Discount rate", "Inflation (Optional. Don't fill in if not available)" };

            public static string Calculate(decimal recPayments, decimal disocuntRate, decimal infl = 0)
            {
                try
                {
                    infl /= infl == 0 ? 1 : 100;
                    disocuntRate /= 100;

                    decimal PerpPrice = Round(recPayments / (disocuntRate - infl), 2);

                    return $"PerpetuityPrice: {PerpPrice}\n" +
                        $"Used formula: Po = C/{(infl == 0 ? "r" : "(r-g)")}\n" +
                        $"Solution: {recPayments}/{(infl == 0 ? $"{disocuntRate}" : $"({disocuntRate} - {infl})")} = {PerpPrice}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        #region Stock
        public static class PreferredStockPrice
        {
            public static readonly string[] Attributes = { "Fixed annual divident", "Discount rate" };

            public static string Calculate(decimal div, decimal discRate)
            {
                try
                {
                    discRate /= 100;
                    decimal StockPrice = Round(div / discRate, 2);

                    return $"Preferred Stock Price: {StockPrice}\n" +
                        $"Used formula: Po = Div/r\n" +
                        $"Solution: {div}/{discRate} = {StockPrice}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        public static class CommonSharePrice
        {
            public static readonly string[] Attributes = { "Divident in the first year", "Rate of return", "Divident increasing norm" };

            public static string Calculate(decimal div, decimal RoR, decimal DIN)
            {
                try
                {
                    RoR /= 100;
                    DIN /= 100;

                    decimal CSP = Round(div * (1 + DIN) / (RoR - DIN), 2);

                    return $"Common share price: {CSP}\n" +
                        $"Used formula: Po = Div/(r-g) = D(1+g)/(r-g)\n" +
                        $"Solution: {div} × (1+{DIN})/({RoR} - {DIN}) = {div * (1 + DIN)}/{RoR - DIN} = {CSP}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        public static class RateOfIncreasing
        {
            public static readonly string[] Attributes = { "Net Profit", "Equity", "Divident repayment factor" };

            public static string Calculate(decimal netProfit, decimal equity, decimal DRF)
            {
                try
                {
                    DRF /= DRF > 0.99m || DRF < -0.99m ? 100 : 1;
                    decimal ROI = Round((netProfit / equity) * (1 - DRF), 2);

                    return $"Rate of increasing: {ROI * 100}%\n" +
                        $"Used formula: g = (NP/E)*(1 - Kp)\n" +
                        $"Solution: ({netProfit}/{equity}) × (1 - {DRF}) = {ROI}";
                }
                catch (OverflowException)
                {
                    return "Impossible Calculation!";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                        "Please check your input.\n" +
                        "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }
        #endregion
    }
}
