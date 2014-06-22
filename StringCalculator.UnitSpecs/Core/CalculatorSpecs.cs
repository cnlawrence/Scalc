using System;
using Moq;
using NUnit.Framework;
using Should;
using SpecsFor;
using StringCalculator.Core.Features;
using StringCalculator.Core.Utilities;

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
                SUT.Add(@"10").ShouldEqual(10);
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

        public class when_calculator_adds_with_user_defined_delimiters : CalculatorSpecs
        {
            [Test]
            public void then_it_should_return_expected_result_with_one_default_delimiter()
            {
                SUT.Add(string.Format(@"//;{0}1;2",Environment.NewLine)).ShouldEqual(3);
            }

            [Test]
            public void then_it_should_return_expected_result_with_one_default_delimiter_longer_than_one_char()
            {
                SUT.Add(string.Format(@"//[***]{0}1***2***3", Environment.NewLine)).ShouldEqual(6);
            }

            [Test]
            public void then_it_should_return_expected_result_with_multiple_default_delimiters()
            {
                SUT.Add(string.Format(@"//[*][%]{0}1*2%3", Environment.NewLine)).ShouldEqual(6);
            }

            [Test]
            public void then_it_should_return_expected_result_with_multiple_default_delimiters_longer_than_one_char()
            {
                SUT.Add(string.Format(@"//[***][$$$$]{0}1***2$$$$3", Environment.NewLine)).ShouldEqual(6);
            }
        }

        public class when_calculator_add_is_called : CalculatorSpecs
        {
            protected override void When()
            {
                SUT.Add(@"1,2");
            }

            [Test]
            public void then_it_should_invoke_loggers_write_function()
            {
                var loggerMock = GetMockFor<ILogger>();
                loggerMock.Verify(l => l.Write(It.IsAny<string>()),Times.Once());
            }
        }

        public class when_calculator_adds_with_negative_numbers : CalculatorSpecs
        {
            [Test]
            public void then_it_should_thow_error_for_one_negative_number_and_returns_expected_exception_message()
            {
                const string expectedMessage = @"negatives not allowed - -1";
                var ex = Assert.Throws<Exception>(() => SUT.Add(@"-1"));
                ex.Message.ShouldEqual(expectedMessage);
            }

            [Test]
            public void then_it_should_thow_error_for_multiple_negative_numbers_and_returns_expected_exception_message()
            {
                const string expectedMessage = @"negatives not allowed - -1 -2";
                var ex = Assert.Throws<Exception>(() => SUT.Add(@"-1,-2"));
                ex.Message.ShouldEqual(expectedMessage);
            }
        }

        public class when_calculator_add_throws_exception : CalculatorSpecs
        {
            protected override void When()
            {
                try
                {
                    SUT.Add(@"-1");
                }
                catch
                {
                    //NOM NOM
                }
            }

            [Test]
            public void then_it_calls_web_service()
            {
                var webServiceMock = GetMockFor<IWebService>();
                webServiceMock.Verify(s => s.PhoneHome(It.IsAny<string>()));
            }
        }

        public class when_calculator_adds_big_numbers : CalculatorSpecs
        {
            [Test]
            public void then_it_adds_only_numbers_less_than_100()
            {
                SUT.Add(@"2,1001").ShouldEqual(2);
            }
        }
    }
}
