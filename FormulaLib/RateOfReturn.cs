using System;
using static System.Math;

namespace Finance
{
    public static class RateOfReturn
    {
        public static readonly string[] Attributes = { "Value of investment", "Future Value" };

        public static string Calculate(decimal valueOfInvestment, decimal futureValue)
        {
            try
            {
                decimal result = ((futureValue - valueOfInvestment) / valueOfInvestment) * 100;
                result = Round(result, 2);

                return $"Rate of return: {result}%\nUsed formula: ((FV-I)/I) × 100 = r%\nSolution: (({futureValue} - {valueOfInvestment}) / {valueOfInvestment}) × 100 = {result}%";
            }
            catch (OverflowException)
            {
                return "Impossible Calculation!";
            }
            catch (DivideByZeroException)
            {
                return "Dividing by zero error!\nPlease check your input.\nIf your input is correct and you get this error, then your calculation is impossible.";
            }
        }
    }
}
