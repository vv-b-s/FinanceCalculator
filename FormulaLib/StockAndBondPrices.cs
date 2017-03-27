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
        public enum CalcType { PresentDiscountBondPrice}

        public static class PresentDiscountBondPrice
        {
            public static string[] Attributes = { "Nominal value", "Selling price", "Rate of return", "Holding period" };

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
    }
}
