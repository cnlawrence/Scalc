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
                const int defaultValue = 0;

                if (numbers != string.Empty)
                {
                    var processedNumbers = ProcessUserInput(numbers);
                    var calculatedValue = processedNumbers.Where(s => s < 1000).Sum();
                    _logger.Write(calculatedValue.ToString(CultureInfo.InvariantCulture));

                    return calculatedValue;
                }

                return defaultValue;
            }
            catch (Exception ex)
            {
                _webService.PhoneHome(string.Format(@"Some kind of error has failed due to : {0}", ex.Message));
                throw;
            }
        }

        private IEnumerable<int> ProcessUserInput(string userInput)
        {
            var userInputScrubbedToNumbers = ScrubDelimiters(userInput);

            var numbersToSumList = userInputScrubbedToNumbers.Split(_defaultDelimitersList.ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var negativeNumbers = numbersToSumList.Where(n => n < 0).ToList();

            ValidateInputForNegativeNumbers(negativeNumbers);

            return numbersToSumList;
        }

        private static void ValidateInputForNegativeNumbers(List<int> negativeNumbers)
        {
            if (negativeNumbers.Any())
            {
                var exceptionMessageList = new List<string> {@"negatives not allowed -"};
                exceptionMessageList.AddRange(negativeNumbers.Select(n => n.ToString(CultureInfo.InvariantCulture)));
                var exceptionMessage = String.Join(" ", exceptionMessageList);

                throw new Exception(exceptionMessage);
            }
        }

        private string ScrubDelimiters(string userInputToProcess)
        {
            var userDelimiterInputRegex = new Regex(@"(?<start>//)(?<userDefinedDelimiters>.*|\[.*\])(?<newLine>.\n)");
            var scrubbedToNumbers = userDelimiterInputRegex.Replace(userInputToProcess, string.Empty);

            if (userDelimiterInputRegex.IsMatch(userInputToProcess))
            {
                var userAssignedDelimiters = userDelimiterInputRegex.Match(userInputToProcess).Groups[2].Value;

                _defaultDelimitersList.Clear();
                _defaultDelimitersList = new List<string> {Environment.NewLine};

                var delimiterRegex = new Regex(@"\[(\W|\w)\1+\]|.");
                foreach (Match match in delimiterRegex.Matches(userAssignedDelimiters))
                {
                    var scrubbedDelimiter = Regex.Replace(match.Value, @"\[|\]", string.Empty);
                    _defaultDelimitersList.Add(scrubbedDelimiter);
                }
            }

            return scrubbedToNumbers;
        }
    }
}
