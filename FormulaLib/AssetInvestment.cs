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
        public enum AssetValues { NetCashFlows, AverageIncomeNorm }

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

        public static class AverageIncomeNorm
        {
            public static readonly string[] Attributes = {"Initial investment cost","Years", "Net Income for each year" };

            public static string Calculate(decimal initInvCost, int years, decimal[] netIncomeEA)
            {
                try
                {
                    decimal averageFutureNI = 0;
                    foreach (var netIncome in netIncomeEA)
                        averageFutureNI += netIncome;
                    averageFutureNI /= years;
                    Round(averageFutureNI, 2);
                    decimal AIN = Round(averageFutureNI / (initInvCost * 0.5m), 2);

                    var sB = new StringBuilder();
                    for (int i = 0; i < years; i++)
                    {
                        if (i < years - 1)
                            sB.Append(netIncomeEA[i] + " + ");
                        else
                            sB.Append(netIncomeEA[i]);
                    }

                    return $"Average Income Norm: {AIN * 100}%\n" +
                        $"Used formula: AIN = Average Future Net Income/(1/2)Initial investment cost\n" +
                        $"Solition:\n" +
                        $"1) Calculating the AFNI:\n" +
                        $"\t({sB.ToString()})/{years} = {averageFutureNI}\n" +
                        $"2) Calculating AIN:\n" +
                        $"\t{averageFutureNI}/({initInvCost} × 0.5) = {AIN}";
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
