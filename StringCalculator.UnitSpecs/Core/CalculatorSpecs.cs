using NUnit.Framework;
using Should;
using SpecsFor;
using StringCalculator.Core.Features;

namespace StringCalculator.UnitSpecs.Core
{
    public abstract class CalculatorSpecs : SpecsFor<Calculator>
    {
        public class when_calculator_adds_with_empty_string : CalculatorSpecs
        {
            private int _result;

            protected override void When()
            {
                _result = SUT.Add(string.Empty);
            }

            [Test]
            public void then_it_returns_zero()
            {
                _result.ShouldEqual(0);
            }
        }
    }
}
