using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketScoreSheetPro.Test.Extension
{
    public sealed class ExpectedExceptionExtension : ExpectedExceptionBaseAttribute
    {
        private Type _expectedExceptionType;
        private string _expectedExceptionMessage;

        public ExpectedExceptionExtension(Type expectedExceptionType)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = string.Empty;
        }

        public ExpectedExceptionExtension(Type expectedExceptionType, string expectedExceptionMessage)
        {
            _expectedExceptionType = expectedExceptionType;
            _expectedExceptionMessage = expectedExceptionMessage;
        }

        protected override void Verify(Exception exception)
        {
            exception.Should().NotBeNull();

            if (exception.GetType() == typeof(AggregateException))
            {
                exception.InnerException.ToString().Should().Contain(_expectedExceptionMessage);
            }
            else
            {
                Assert.IsInstanceOfType(exception, _expectedExceptionType, "Wrong type of exception was thrown.");
                if (!_expectedExceptionMessage.Length.Equals(0))
                {
                    exception.Message.Should().Contain(_expectedExceptionMessage);
                }
            }
        }
    }
}
