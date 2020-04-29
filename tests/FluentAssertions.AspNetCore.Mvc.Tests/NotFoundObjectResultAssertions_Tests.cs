﻿using System;
using FluentAssertions.Mvc.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FluentAssertions.AspNetCore.Mvc.Tests
{
    public class NotFoundObjectResultAssertions_Tests
    {
        private const string TestValue = "testValue";
        [Fact]
        public void Value_GivenExpectedValue_ShouldPass()
        {
            var result = new TestController().NotFound(TestValue);
            result.Should().BeNotFoundObjectResult().Value.Should().Be(TestValue);
        }

        [Fact]
        public void Value_GivenUnexpectedValue_ShouldFail()
        {
            var result = new TestController().NotFound(TestValue);

            Action a = () => result.Should().BeNotFoundObjectResult().Value.Should().Be("xyx");
            a.Should().Throw<Exception>();
        }

        [Fact]
        public void ValueAs_GivenExpectedValue_ShouldPass()
        {
            var result = new TestController().NotFound(TestValue);

            result.Should().BeNotFoundObjectResult().ValueAs<string>().Should().Be(TestValue);
        }

        [Fact]
        public void ValueAs_GivenUnexpectedValue_ShouldFail()
        {
            var result = new TestController().NotFound(TestValue);

            Action a = () => result.Should().BeNotFoundObjectResult().ValueAs<string>().Should().Be("xyx");
            a.Should().Throw<Exception>();
        }

        [Fact]
        public void ValueAs_GivenWrongType_ShouldFail()
        {
            var result = new TestController().NotFound(TestValue);
            string failureMessage = FailureMessageHelper.ExpectedContextTypeXButFoundY(
                "NotFoundObjectResult.Value", typeof(int).FullName, typeof(string).FullName);

            Action a = () => result.Should().BeNotFoundObjectResult().ValueAs<int>().Should().Be(2);

            a.Should().Throw<Exception>()
                .WithMessage(failureMessage);
        }

        [Fact]
        public void ValueAs_Null_ShouldFail()
        {
            ActionResult result = new NotFoundObjectResult(null);
            string failureMessage = FailureMessageHelper.ExpectedContextTypeXButFoundNull(
                "NotFoundObjectResult.Value", typeof(object).FullName);

            Action a = () => result.Should().BeNotFoundObjectResult().ValueAs<object>();

            a.Should().Throw<Exception>()
                .WithMessage(failureMessage);
        }
    }
}
