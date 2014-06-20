using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace StringCalculator.Core.Setup.Scanners
{
    public class GenericScanner : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.IsInterface && type.Name.StartsWith("I"))
            {
                registry.Policies.SetAllProperties(x => x.TypeMatches(z => z == type));
            }
        }
    }
}
