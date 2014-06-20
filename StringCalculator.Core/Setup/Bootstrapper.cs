using StringCalculator.Core.Setup.Registries;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace StringCalculator.Core.Setup
{
    public class Bootstrapper
    {
        public static void Init()
        {
            BootstrapCommon();
        }

        private static void BootstrapCommon()
        {
            ObjectFactory.Initialize(x => x.AddRegistry<ConventionalRegistry>());
        }

        public static void AddFeatureRegistry<TRegistry>() where TRegistry : Registry
        {
            var registry = ObjectFactory.GetInstance<TRegistry>();

            ObjectFactory.Configure(x => x.AddRegistry(registry));
        }
    }
}
