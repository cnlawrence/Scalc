using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using StringCalculator.Core.Utilities;

namespace StringCalculator.Core.Features
{
    public class Calculator : ICalculator
    {
        private readonly ILogger _logger;
        private readonly IWebService _webService;
        private List<string> _defaultDelimitersList = new List<string> {Environment.NewLine,","};

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

        private int CalculateNumbers(string numbers)
        {
            const string userDelimiterRegex = @"(?<start>//)(?<userDefinedDelimiters>.*)(?<newLine>.\n)";
            if (Regex.IsMatch(numbers,userDelimiterRegex))
            {
                var userAssignedDelimiter = Regex.Match(numbers, userDelimiterRegex).Groups[2].Value;
                _defaultDelimitersList.Clear();
                _defaultDelimitersList = new List<string> { Environment.NewLine, userAssignedDelimiter };
                numbers = Regex.Replace(numbers, userDelimiterRegex, string.Empty); 
            }

            var numbersToSumList = numbers.Split(_defaultDelimitersList.ToArray(), StringSplitOptions.None).ToList();

            return numbersToSumList.Select(int.Parse).Sum();
        }
    }
}
