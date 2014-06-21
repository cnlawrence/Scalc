using System;
using System.Linq;
using StringCalculator.Core.Utilities;

namespace StringCalculator.Core.Features
{
    public class Calculator : ICalculator
    {
        private readonly ILogger _logger;
        private readonly IWebService _webService;
        private static readonly string[] DefaultDelimiter = {","};

        public Calculator(ILogger logger, IWebService webService)
        {
            _logger = logger;
            _webService = webService;
        }

        public int Add(string numbers)
        {
            const int defaultValue = 0;

            if (numbers.Length == 0 || numbers == string.Empty)
                return defaultValue;

            return CalculateNumbers(numbers);
        }

        private static int CalculateNumbers(string numbers)
        {
            var numbersToSum = numbers.Split(DefaultDelimiter, new StringSplitOptions()).ToList();

            return numbersToSum.Select(int.Parse).Sum();
        }
    }
}
