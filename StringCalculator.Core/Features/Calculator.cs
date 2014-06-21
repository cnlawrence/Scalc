using System;
using System.Linq;
using System.Text.RegularExpressions;
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
            var scrubbedNumbers = ScrubNewLines(numbers);

            var numbersToSum = scrubbedNumbers.Split(DefaultDelimiter, new StringSplitOptions()).ToList();

            return numbersToSum.Select(int.Parse).Sum();
        }

        private static string ScrubNewLines(string numbers)
        {
            var newLineRegEx = new Regex(@"\n");
            return newLineRegEx.Replace(numbers, DefaultDelimiter[0]);
        }
    }
}
