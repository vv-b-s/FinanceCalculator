using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Finance
{
    public class AssetInvestment
    {
        public enum AssetValues { NetCashFlows}

        public static class NetCashFlows
        {
            public static readonly string[] Attributes = { "Revenues", "Variable costs", "Fixed costs", "Deprication", "Corporative Taxes" };

            public static string Calculate(decimal revenues, decimal variableCosts, decimal fixedCosts, decimal deprication, decimal corpTaxes)
            {
                try
                {
                    corpTaxes /= corpTaxes <= 0.99m && corpTaxes >= -0.99m ? 1 : 100;
                    decimal CTP = revenues - variableCosts - fixedCosts - deprication;             // Conditional taxable profit
                    decimal NP = Round(CTP * (1 - corpTaxes), 2);                                 // Net Profit
                    decimal NetCashFlows = NP + deprication;

                    return $"Net Cash Flows: {NetCashFlows}\n" +
                        $"Used formula: NCF = Net Profit + Deprication\n" +
                        "Solution:\n" +
                        "1) Defining conditional taxable profit:\n" +
                        "\tCTP = Revenues - Variable Costs - Fixed Costs - Deprication\n" +
                        $"\t{revenues} - {variableCosts} - {fixedCosts} - {deprication} = {CTP}\n\n" +
                        $"2) Calculating the net profit:\n" +
                        $"\tNP = CTP × (1 - Corporative Taxes)\n" +
                        $"\t{CTP} × (1-{corpTaxes} = {NP}\n\n" +
                        $"3) Calculating the net cash flows\n" +
                        $"\t{NP} + {deprication} = {NetCashFlows}";
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
            }
        }
    }
}
