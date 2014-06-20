using StringCalculator.Core.Setup.Scanners;
using StructureMap.Configuration.DSL;

namespace StringCalculator.Core.Setup.Registries
{
    public class ConventionalRegistry : Registry
    {
        public ConventionalRegistry()
        {
            Scan(scan =>
            {
                scan.With(new GenericScanner());
                scan.WithDefaultConventions();
            });
        }
    }
}
