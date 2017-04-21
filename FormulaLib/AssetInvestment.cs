using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using TextTable;

namespace Finance
{
    public class AssetInvestment
    {
        public enum AssetValues { NetCashFlows, AverageIncomeNorm, NetPresentValue, ProfitabilityIndex, PaybackPeriod }

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

                    return $"Net Cash Flows: {NetCashFlows:C2}\n" +
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
                        $"\t{averageFutureNI}/({initInvCost} × 0.5) = {AIN}\n\n" +
                        $"Notice: The project with the highest AIN is the one which should be accepted.";
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

        public static class NetPresentValue
        {
            public static string[] Attributes = { "Comulative investitional costs", "Discount norm", "Years", "Cash flows for each year" };

            public static string Calculate(decimal comulInvCosts, decimal discountNorm, int years, decimal[] cashFlowsEA)
            {
                try
                {
                    discountNorm /= discountNorm <= 0.99m && discountNorm >= -0.99m ? 1 : 100;
                    decimal cashFlowsSum = 0;
                    for (int i = 0; i < years; i++)
                        cashFlowsSum += cashFlowsEA[i] / (decimal)Pow((double)(1 + discountNorm), i + 1);
                    cashFlowsSum = Round(cashFlowsSum, 2);

                    decimal NPV = cashFlowsSum - comulInvCosts;

                    return $"Net Present Value: {Round(NPV, 2):C2}\n" +
                        $"Used formula: {(char)931} Fn/(1+r)^n - {(char)931}In/(1+r)^n\n" +
                        $"Solution:\n" +
                        $"{CreateTable(years, discountNorm, cashFlowsEA, comulInvCosts)}\n\n" +
                        $"NPV = {Round(cashFlowsSum, 2)} - {Round(comulInvCosts, 2)} = {Round(NPV, 2)}";
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

            private static string CreateTable(int years, decimal discountNorm, decimal[] cashFlowsEA, decimal comulInvCosts)
            {
                var outputTable = Table.Create(years + 2, 4, true, true);

                outputTable.Modify(0, 0) = "Year"; outputTable.Modify(0, 1) = "NCF"; outputTable.Modify(0, 2) = $"PV of 1 unit with {discountNorm * 100}% discount"; outputTable.Modify(0, 3) = "PV each year";

                for(int i=0;i<years;i++)
                {
                    outputTable.Modify(i + 1, 0) = (i + 1).ToString();
                    outputTable.Modify(i + 1, 1) = cashFlowsEA[i].ToString();
                    outputTable.Modify(i + 1, 2) = Round(1 / Pow((double)(1 + discountNorm), i+1), 3).ToString();
                    outputTable.Modify(i + 1, 3) = Round(cashFlowsEA[i] / (decimal)Pow((double)(1 + discountNorm), i + 1), 2).ToString();
                }

                decimal sumAll = 0;
                for (int i = 0; i < years; i++)
                    sumAll += decimal.Parse(outputTable.Modify(i+1,3));

                outputTable.Modify(years + 1, 0) = "Sum";
                outputTable.Modify(years + 1, 3) = sumAll.ToString();

                return outputTable.ToString();
            }
        }

        public static class ProfitabilityIndex
        {
            public static readonly string[] Attributes = NetPresentValue.Attributes;

            public static string Calculate(decimal comulInvCosts, decimal discountNorm, int years, decimal[] cashFlowsEA)
            {
                try
                {
                    discountNorm /= discountNorm <= 0.99m && discountNorm >= -0.99m ? 1 : 100;
                    decimal cashFlowsSum = 0;
                    for (int i = 0; i < years; i++)
                        cashFlowsSum += cashFlowsEA[i] / (decimal)Pow((double)(1 + discountNorm), i + 1);
                    cashFlowsSum = Round(cashFlowsSum, 2);

                    decimal PI = Round(cashFlowsSum / comulInvCosts, 3);

                    return $"Profitability Index: {PI}\n" +
                        $"Used formula: {(char)931} Fn/(1+r)^n / {(char)931}In/(1+r)^n\n" +
                        $"Solution: {cashFlowsSum} / {comulInvCosts} = {PI}";
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

        public static class PaybackPeriod
        {
            public static string[] Attributes = NetPresentValue.Attributes;

            public static string Calculate(decimal comulInvCosts, decimal discountNorm, int years, decimal[] cashFlowsEA)
            {
                try
                {
                    discountNorm /= discountNorm > 0.99m || discountNorm < -0.99m ? 100 : 1;

                    #region Calculations
                    decimal[,] calculations = new decimal[years, 4];
                    int fullPayback = 0;

                    for (int i = 0; i < years; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            switch (j)
                            {
                                case 0: calculations[i, j] = i + 1; break;
                                case 1: calculations[i, j] = cashFlowsEA[i]; break;
                                case 2: calculations[i, j] = cashFlowsEA[i] / (decimal)Factors.CompInterestFactor(i + 1, (double)discountNorm); break;
                                case 3: calculations[i, j] = i == 0 ? calculations[i, j - 1] : calculations[i - 1, j] + calculations[i, j - 1]; break;
                            }
                        }

                        if (calculations[i, 3] > comulInvCosts && fullPayback == 0)
                            fullPayback = i;
                    }

                    int paybackYears = fullPayback;
                    decimal paybackMonths = (comulInvCosts - calculations[fullPayback - 1, 3]) / calculations[fullPayback, 2];
                    paybackMonths = Round(paybackMonths * 12);
                    #endregion

                    #region Output
                    Table oTable = Table.Create(years + 1, 4, true, false);
                    oTable.Modify(0, 0) = "Period";
                    oTable.Modify(0, 1) = "NCF";
                    oTable.Modify(0, 2) = "Discounted NCF";
                    oTable.Modify(0, 3) = "Comul.";

                    for (int i = 1; i <= years; i++)
                        for (int j = 0; j < 4; j++)
                            oTable.Modify(i, j) = j == 0 ? calculations[i - 1, j].ToString() : Round(calculations[i - 1, j]).ToString("C2");

                    return $"For your payback it will take {(paybackMonths >= 12 ? paybackYears + 1 : paybackYears)} years and {(paybackMonths >= 12 ? 0 : paybackMonths)} months.\n\n" +
                           $"{oTable.ToString()}";
                    #endregion
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
