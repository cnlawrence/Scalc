using System;
using System.Collections.Generic;
using System.Globalization;
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
            try
            {
                var calculatedValue = CalculateNumbers(numbers);
                _logger.Write(calculatedValue.ToString(CultureInfo.InvariantCulture));

                return calculatedValue;
            }
            catch (Exception ex)
            {
                _webService.PhoneHome(string.Format(@"Some kind of error has failed due to : {0}", ex.Message));
                throw;
            }
        }

        private int CalculateNumbers(string numbers)
        {
            const int defaultValue = 0;

            if (numbers != string.Empty)
            {
                const string userDelimiterRegex = @"(?<start>//)(?<userDefinedDelimiters>.*)(?<newLine>.\n)";
                if (Regex.IsMatch(numbers, userDelimiterRegex))
                {
                    var userAssignedDelimiter = Regex.Match(numbers, userDelimiterRegex).Groups[2].Value;
                    _defaultDelimitersList.Clear();
                    _defaultDelimitersList = new List<string> {Environment.NewLine, userAssignedDelimiter};
                    numbers = Regex.Replace(numbers, userDelimiterRegex, string.Empty);
                }

                var numbersToSumList = numbers.Split(_defaultDelimitersList.ToArray(), StringSplitOptions.None).ToList();

                var negativeNumbers = numbersToSumList.Where(n => int.Parse(n) < 0).ToList();

                if (negativeNumbers.Any())
                {
                    var exceptionMessageList = new List<string> {@"negatives not allowed -"};
                    exceptionMessageList.AddRange(negativeNumbers);
                    var exceptionMessage = String.Join(" ", exceptionMessageList);

                    throw new Exception(exceptionMessage);
                }

                return numbersToSumList.Select(int.Parse).Where(s => s < 1000).Sum();
            }

            return defaultValue;
        }
    }
}
