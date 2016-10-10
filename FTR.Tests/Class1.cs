using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FTR.Services;
using NSubstitute;
using NUnit.Framework;
using TestStack.BDDfy;

namespace FTR.Tests
{
    [TestFixture(Category = "UnitTest")]
    public class UiPrinterTest
    {
        private IOutputWriter _mockConsole;
        private UiPrinter _testFixture;

        [SetUp]
        public void SetUp()
        {
            _mockConsole = NSubstitute.Substitute.For<IOutputWriter>();
            _testFixture = new UiPrinter(_mockConsole);
        }
        public void ItShouldPrintFibOnFibonacciNumberEvent()
        {
            FluentStepScannerExtensions
                .When(this, x => WhenTheOutputsOnFibonacciNumberEventIsCalled())
                .Then(x => ThenTheConsoleWillPrint("FIB"))
                .BDDfy();

        }

        private void ThenTheConsoleWillPrint(string text)
        {
            _mockConsole.Received().WriteLine(text);
        }

        private void WhenTheOutputsOnFibonacciNumberEventIsCalled()
        {
            _testFixture.OnFibonacciNumber(this, EventArgs.Empty);
        }
    }
}
