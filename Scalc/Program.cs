using StringCalculator.Core.Setup;

namespace Scalc
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Handle arguments

            ConfigureContainer();
        }

        private static void ConfigureContainer()
        {
            Bootstrapper.Init();
        }
    }
}
