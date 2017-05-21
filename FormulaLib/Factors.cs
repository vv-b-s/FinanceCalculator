using System;
using static System.Math;
using TextTable;

namespace Finance
{
    public class Factors
    {
        public delegate double Method(int years, double interestRate);

        public static readonly string[] Attributes = { "Years", "Interest rate" }; 

        public static double CompInterestFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate >= -0.99 ? 1 : 100;
            return Round(Pow(1 + interestRate, years), 3);
        }

        public static double DiscountFactor(int years, double interestRate) => Round(1 / CompInterestFactor(years, interestRate), 3);

        public static double AnnuityFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate >= -0.99 ? 1 : 100;

            return Round(Pow((1 + interestRate), years) / interestRate, 3);
        }

        public static double DiscountAnnuityFactor(int years, double interestRate)
        {
            interestRate /= interestRate <= 0.99 && interestRate >= -0.99 ? 1 : 100;

            return Round(1 / interestRate - 1 / (interestRate * CompInterestFactor(years, interestRate)), 3);
        }

        public static string GetTable(int years, double interestRate, Method A, Method B)               // Using delegate just to experiment
        {
            Table table = Table.Create(3, 3, true, false);
            table.Modify(0, 1) = "Start Period";
            table.Modify(0, 2) = "End Period";
            table.Modify(1, 0) = "AF";
            table.Modify(1, 1) = (Round(A(years, interestRate) * (1 + interestRate), 3)).ToString();
            table.Modify(1, 2) = A(years, interestRate).ToString();
            table.Modify(2, 0) = "DAF";
            table.Modify(2, 1) = (Round(B(years, interestRate) * (1 + interestRate), 3)).ToString();
            table.Modify(2, 2) = B(years, interestRate).ToString();

            return table.ToString();
        }

        public static string ShowFactors(int years, double interestRate)
        {
            try
            {
                return $"Compound interest factor: {CompInterestFactor(years, interestRate)}\n" +
                        $"Discount interest factor: {DiscountFactor(years, interestRate)}\n\n" +
                        $"Annuity and Discount Annuity factors:\n{GetTable(years, interestRate, AnnuityFactor, DiscountAnnuityFactor)}";
                        
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
