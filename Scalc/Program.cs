using System;
using StringCalculator.Core.Features;
using StringCalculator.Core.Utilities;
using StructureMap;

namespace Scalc
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureContainer();

            CallCalculator(args[0]);
            
            var userInput = Console.ReadLine();

            while (userInput.Length > 0)
            {
                CallCalculator(userInput);
                userInput = Console.ReadLine();
            }
        }

        private static void CallCalculator(string userInput)
        {
            var calculator = ObjectFactory.GetInstance<Calculator>();
            
            var result = String.Format(@"The result is {0}", calculator.Add(userInput.Replace("'", string.Empty)));
            Console.WriteLine(result);
            Console.Write("another input please ");
        }

        private static void ConfigureContainer()
        {
            ObjectFactory.Configure(cfg =>
                {
                    cfg.For<ICalculator>().Use<Calculator>();
                    cfg.For<ILogger>().Use<Logger>();
                    cfg.For<IWebService>().Use<WebService>();
                });
        }
    }
}
