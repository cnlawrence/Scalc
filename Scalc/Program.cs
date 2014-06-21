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

            var calculator = ObjectFactory.GetInstance<Calculator>();

            foreach (var a in args)
            {
                var result = String.Format(@" The result is {0}", calculator.Add(a.Replace("'",string.Empty)));
                Console.WriteLine(result);
            }
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
