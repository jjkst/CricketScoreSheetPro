using System;
using CricketScoreSheetPro.Core.Helper;
using CricketScoreSheetPro.Test.Extension;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CricketScoreSheetPro.Test.UnitTest
{
    [TestClass]
    public class FunctionTest
    {
        [TestMethod]
        [ExpectedExceptionExtension(typeof(ArgumentException), "Balls cannot be negative.")]
        [TestCategory("UnitTest")]
        public void BallsToOversValueConverter_NegativeBalls()
        {
            //Act
            var val = Function.BallsToOversValueConverter(-2);

        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void BallsToOversValueConverter_ZeroBalls()
        {
            //Act
            var val = Function.BallsToOversValueConverter(0);

            //Assert
            val.Should().Be("0.0");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void BallsToOversValueConverter_BallsWhenOverisnotDone()
        {
            //Act
            var val = Function.BallsToOversValueConverter(16);

            //Assert
            val.Should().Be("2.4");
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void BallsToOversValueConverter_BallsWhenOverisDone()
        {
            //Act
            var val = Function.BallsToOversValueConverter(24);

            //Assert
            val.Should().Be("4.0");
        }
    }
}
