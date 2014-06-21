using System;
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
            [Test]
            public void then_it_returns_expected_result_with_three_inputs()
            {
                SUT.Add(@"1,2,3").ShouldEqual(6);
            }

            [Test]
            public void then_it_returns_expected_result_with_two_inputs()
            {
                SUT.Add(@"1,2").ShouldEqual(3);
            }

            [Test]
            public void then_it_returns_expected_result_with_one_input()
            {
                SUT.Add(@"1701").ShouldEqual(1701);
            }
        }

        public class when_calculator_adds_with_new_line_character_instead_of_delimeter : CalculatorSpecs
        {
            [Test]
            public void then_it_returns_expected_result_with_one_new_line_character()
            {
                SUT.Add(String.Format(@"1{0}2,3", Environment.NewLine)).ShouldEqual(6);
            }

            [Test]
            public void then_it_returns_expected_result_with_two_new_line_character()
            {
                SUT.Add(String.Format(@"1{0}2{0}3", Environment.NewLine)).ShouldEqual(6);
            }
        }

        public class when_calculator_adds_with_different_delimiter : CalculatorSpecs
        {
            [Test]
            public void then_it_should_return_expected_result_with_new_default_delimiter()
            {
                SUT.Add(string.Format(@"//;{0}1;2",Environment.NewLine)).ShouldEqual(3);
            }
        }
    }
}
