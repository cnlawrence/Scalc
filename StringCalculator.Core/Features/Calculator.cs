using StringCalculator.Core.Utilities;

namespace StringCalculator.Core.Features
{
    public class Calculator : ICalculator
    {
        private readonly ILogger _logger;
        private readonly IWebService _webService;

        public Calculator(ILogger logger, IWebService webService)
        {
            _logger = logger;
            _webService = webService;
        }

        public int Add(string numbers)
        {
            const int defaultValue = 0;

            if (numbers.Length == 0 || numbers == string.Empty)
            {
                return defaultValue;
            }

            return defaultValue;
        }
    }
}
