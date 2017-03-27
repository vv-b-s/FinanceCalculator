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
        public enum CalcType { PresentDiscountBondPrice, CuponBondPrice}

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
                        $"\n\nThe bond {(presentValue - sellingPrice>0?"can be acquired!":"shouldn't be acqired!")}";
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
    }
}
