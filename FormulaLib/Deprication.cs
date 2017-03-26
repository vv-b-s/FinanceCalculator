/*Notice: if there are any problems with the table,
 *use Font: Courier New (as it simply works)
 * or visit http://stackoverflow.com/questions/763257/string-padding-problem
 */
using System;
using static System.Math;

using TextTable;

namespace Finance
{
    public static class Deprication
    {
        public enum DepricationType { Linear, DecreasingDeduction, ComulativeMethod, EqualDegression }

        public static class LinearDeprication
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years", "Liquidation value" };

            public static string Calculate(decimal AC, decimal TaxRate, int Years)
            {
                try
                {
                    decimal LD = Round(100m / Years, 2);

                    return $"Linear Deprication Norm: {LD}%\n" +
                           "Used fromula: 100/Years\n" +
                           $"Solution: 100 / {Years} = {LD}%\n\n" + CreateTable(LD, AC, Years, TaxRate);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                           "Please check your input.\n" +
                           "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            public static string Calculate(decimal AC, decimal TaxRate, int Years, decimal LV)
            {
                try
                {
                    decimal LD = Round(((AC - LV) / (AC * Years)) * 100, 2);
                    return $"Linear Deprication Norm: {LD}%\n" +
                           "Used fromula: [(AC-LV)/(AC × Years)] × 100\n" +
                           $"Solution: [({AC} - {LV}) /({AC} × {Years}] × 100 = {LD}%\n\n" + CreateTable(LD, AC, Years, TaxRate);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                           "Please check your input.\n" +
                           "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            private static string CreateTable(decimal LD, decimal AC, int Years, decimal TaxRate)
            {
                TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;
                LD = LD / 100;
                decimal DepricationDeduction = AC * LD;

                var output = Table.Create(Years + 2, 5, true, true);

                #region Head
                output.Modify(0, 0) = $"Year";
                output.Modify(0, 1) = $"Depr. Norm";
                output.Modify(0, 2) = $"{(char)8721}{"Depr. Deduct."}";
                output.Modify(0, 3) = $"{"Comulative Sum"}";
                output.Modify(0, 4) = "Tax Savings";
                #endregion

                #region Body
                for (int i = 1; i <= Years; i++)
                    for (int k = 0; k < 5; k++)
                    {
                        switch (k)
                        {
                            case 0:
                                output.Modify(i, k) = $"{i}"; break;
                            case 1:
                                output.Modify(i, k) = $"{LD * 100:0.00}"; break;
                            case 2:
                                output.Modify(i, k) = $"{DepricationDeduction:0.00}"; break;
                            case 3:
                                output.Modify(i, k) = $"{DepricationDeduction * i:0.00}"; break;
                            case 4:
                                output.Modify(i, k) = $"{DepricationDeduction * TaxRate:0.00}"; break;
                        }
                    }
                #endregion

                #region Legs
                output.Modify(Years + 1, 0) = $"Total";
                output.Modify(Years + 1, 1) = $"100";
                output.Modify(Years + 1, 2) = $"{AC:0.00}";
                output.Modify(Years + 1, 3) = $"-";
                output.Modify(Years + 1, 4) = $"{DepricationDeduction:0.00}";
                #endregion

                return output.ToString();
            }
        }

        public static class DecreasingDeduction
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years", "Increase coefficient" };

            public static string Calculate(decimal AC, decimal TaxRate, int Years, decimal IC)
            {
                if (IC < 1.5m || IC > 2.2m)
                    return $"Increase coefficient can be between {1.5} and {2.2}.";
                try
                {
                    decimal LinearDepricationNorm = Round(100m / Years, 2);
                    decimal DepricationNorm = LinearDepricationNorm * IC;

                    return $"Deprication Norm: {DepricationNorm}%\n" +
                           "Used formula: (100 / Years) × K\n" +
                           $"Solution: (100 / {Years}) = {LinearDepricationNorm} × {IC} = {DepricationNorm}%\n\n" + CreateTable(DepricationNorm, AC, Years, TaxRate);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                           "Please check your input.\n" +
                           "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }

            private static string CreateTable(decimal DDeduction, decimal AC, int Years, decimal TaxRate)
            {
                TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;
                DDeduction /= 100;
                decimal DepricationDeduction = 0;
                decimal comulSum = DepricationDeduction;

                var output = Table.Create(Years + 2, 5, true, true);

                #region Head
                output.Modify(0, 0) = $"{"Year"}";
                output.Modify(0, 1) = $"{"Depr. Norm"}";
                output.Modify(0, 2) = $"{"Deprication"}";
                output.Modify(0, 3) = $"{"Comulative Sum"}";
                output.Modify(0, 4) = "Tax Savings";
                #endregion

                #region Body
                for (int i = 1; i <= Years; i++)
                {
                    DepricationDeduction = i > 0 && i <= Years - 2 ? AC * DDeduction : AC * 1 / ((i == Years - 1) ? 2 : 1);         // Last two years are deducted with 50%                      
                    comulSum += DepricationDeduction;
                    AC -= DepricationDeduction;

                    for (int k = 0; k < 5; k++)
                        switch (k)
                        {
                            case 0:
                                output.Modify(i, k) = $"{i}"; break;
                            case 1:
                                output.Modify(i, k) = $"{(i <= Years - 2 ? DDeduction * 100 : 50):0.00}"; break;
                            case 2:
                                output.Modify(i, k) = $"{DepricationDeduction:0.00}"; break;
                            case 3:
                                output.Modify(i, k) = $"{comulSum:0.00}"; break;
                            case 4:
                                output.Modify(i, k) = $"{DepricationDeduction * TaxRate:0.00}"; break;
                        }
                }
                #endregion

                #region Legs
                output.Modify(Years + 1, 0) = $"Total";
                output.Modify(Years + 1, 1) = $"{"-"}";
                output.Modify(Years + 1, 2) = $"{comulSum:0.00}";
                output.Modify(Years + 1, 3) = $"{"-"}";
                output.Modify(Years + 1, 4) = $"{comulSum * (100 / Years) / 100:0.00}";
                #endregion

                return output.ToString();
            }
        }

        public static class ComulativeMethod
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years" };

            public static string Calculate(decimal AC, decimal TaxRate, int Years) => CreateTable(AC, Years, TaxRate);

            private static string CreateTable(decimal AC, int Years, decimal TaxRate)
            {
                try
                {
                    var output = Table.Create(Years + 2, 6, true, true);
                    decimal comulativeSum = 0;
                    TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;

                    int yearSum = 0;
                    for (int i = Years; i > 0; i--)
                        yearSum += i;

                    #region Head
                    output.Modify(0, 0) = $"Year";
                    output.Modify(0, 1) = $"Years left";
                    output.Modify(0, 2) = $"Depr. Norm";
                    output.Modify(0, 3) = $"Deprication";
                    output.Modify(0, 4) = $"Comulative Sum";
                    output.Modify(0, 5) = "Tax Savings";
                    #endregion

                    #region Body
                    for (int i = 1, j = Years; i <= Years; i++, j--)
                    {
                        decimal depricationRate = Round(j / (decimal)yearSum * 100m, 2);
                        decimal DepricationDeduction = AC * (depricationRate / 100m);
                        comulativeSum += DepricationDeduction;

                        for (int k = 0; k < 6; k++)
                        {
                            switch (k)                      // To define what each cell should contain.
                            {
                                case 0:
                                    output.Modify(i, k) = i.ToString(); break;
                                case 1:
                                    output.Modify(i, k) = j.ToString(); break;
                                case 2:
                                    output.Modify(i, k) = $"{j}/{yearSum} × 100 = {depricationRate:0.00}"; break;
                                case 3:
                                    output.Modify(i, k) = $"{DepricationDeduction:0.00}"; break;
                                case 4:
                                    output.Modify(i, k) = $"{comulativeSum:0.00}"; break;
                                case 5:
                                    output.Modify(i, k) = $"{DepricationDeduction * TaxRate:0.00}"; break;

                            }
                        }
                    }

                    #endregion

                    #region Legs
                    output.Modify(Years + 1, 0) = $"Total";
                    output.Modify(Years + 1, 1) = $"{yearSum}";
                    output.Modify(Years + 1, 2) = $"{"-"}";
                    output.Modify(Years + 1, 3) = $"{comulativeSum:0.00}";
                    output.Modify(Years + 1, 4) = $"{"-"}";
                    output.Modify(Years + 1, 5) = $"{AC * (100m / Years) / 100:0.00}";

                    #endregion

                    string additionalInfo = "\n\nNotice that the Аcquisition cost remains constant.";

                    return string.Concat(output.ToString(), additionalInfo);
                }
                catch (OverflowException)
                {
                    return "Impossible calculation";
                }
                catch (DivideByZeroException)
                {
                    return "Dividing by zero error!\n" +
                           "Please check your input.\n" +
                           "If your input is correct and you get this error, then your calculation is impossible.";
                }
            }
        }

        public static class EqualDegression
        {
            public static readonly string[] Attributes = { "Аcquisition cost", "Tax Rate", "Years" };

            static int Years = 0;
            static decimal CoE                                  // Coefficient of equality. used to calculate the deprication norm each year.
            {
                get
                {
                    int eqC = 2;
                    for (int i = 1, j = 2; i < Years; i++)
                        eqC += j + i;
                    return Round(100m / eqC, 2);
                }
            }

            public static string Calculate(decimal AC, decimal TaxRate, int years) => CreateTable(AC, years, TaxRate);

            static string CreateTable(decimal AC, int years, decimal TaxRate)
            {
                TaxRate = TaxRate > 0.99m || TaxRate < -0.99m ? TaxRate / 100 : TaxRate;
                Years = years;
                decimal DDeduction = CoE / 100;
                decimal comulSum = 0;

                var output = Table.Create(Years + 2, 5, true, true);

                #region Head
                output.Modify(0, 0) = $"Year";
                output.Modify(0, 1) = $"Depr. Norm";
                output.Modify(0, 2) = $"{(char)8721}{"Depr. Deduct."}";
                output.Modify(0, 3) = $"{"Comulative Sum"}";
                output.Modify(0, 4) = "Tax Savings";
                #endregion

                #region Body
                for (int i = 1, j = years + 1; i <= Years; i++, j--)
                {
                    comulSum += AC * DDeduction * j;
                    for (int k = 0; k < 5; k++)
                    {
                        switch (k)
                        {
                            case 0:
                                output.Modify(i, k) = $"{i}"; break;
                            case 1:
                                output.Modify(i, k) = $"{j * CoE:0.00}"; break;
                            case 2:
                                output.Modify(i, k) = $"{Round(AC * DDeduction * j, 2):0.00}"; break;
                            case 3:
                                output.Modify(i, k) = $"{Round(comulSum, 2):0.00}"; break;
                            case 4:
                                output.Modify(i, k) = $"{Round(AC * DDeduction * j * TaxRate, 2):0.00}"; break;
                        }
                    }
                }
                #endregion

                #region Legs
                output.Modify(Years + 1, 0) = $"Total";
                output.Modify(Years + 1, 1) = $"100";
                output.Modify(Years + 1, 2) = $"{AC:0.00}";
                output.Modify(Years + 1, 3) = $"-";
                output.Modify(Years + 1, 4) = $"{AC * TaxRate:0.00}";
                #endregion

                return output.ToString();
            }
        }
    }
}
