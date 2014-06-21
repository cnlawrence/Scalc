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

        public class when_calculator_adds_with_default_delimiter : CalculatorSpecs
        {
            private int _result;

            [Test]
            public void then_it_returns_expected_result_with_three_inputs()
            {
                _result = SUT.Add(@"1,2,3");
                _result.ShouldEqual(6);
            }

            [Test]
            public void then_it_returns_expected_result_with_two_inputs()
            {
                _result = SUT.Add(@"1,2");
                _result.ShouldEqual(3);
            }

            [Test]
            public void then_it_returns_expected_result_with_one_input()
            {
                _result = SUT.Add(@"1701");
                _result.ShouldEqual(1701);
            }
        }
    }
}
