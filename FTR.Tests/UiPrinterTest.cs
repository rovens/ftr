using System;
using FTR.Services;
using NSubstitute;
using NUnit.Framework;
using TestStack.BDDfy;
using TestStack.BDDfy.Annotations;

namespace FTR.Tests
{
    [TestFixture(Category = "UnitTest")]
    public class UiPrinterTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockConsole = Substitute.For<IOutput>();
            _gameState = Substitute.For<IGameState>();
            _testFixture = new UiPrinter(_mockConsole, _gameState);
        }

        private IOutput _mockConsole;
        private UiPrinter _testFixture;
        private IGameState _gameState;

        private void GivenAGameHasBeenStarted()
        {
            _testFixture.DisplayNumberHistoryFrequency(TimeSpan.FromSeconds(10));
        }

        private void WhenTheGameStateChangesToPauseIs(bool state)
        {
            _testFixture.OnGameStateChange(this, new GameStateEventArgs {IsPaused = state});
        }

        private void ThenTheConsoleWillPrint(string text)
        {
            _mockConsole.Received().WriteLine(text);
        }

        private void WhenTheOutputsOnFibonacciNumberEventIsCalled()
        {
            _testFixture.OnFibonacciNumber(this, EventArgs.Empty);
        }

        [Test]
        public void ItShouldPrintFibOnFibonacciNumberEvent()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .When(x => WhenTheOutputsOnFibonacciNumberEventIsCalled())
                .Then(x => ThenTheConsoleWillPrint("FIB"))
                .BDDfy();
        }

        [Test]
        public void ItShouldPrintTimerHaltedWhenTheGameStateChangesToPaused()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .When(x => WhenTheGameStateChangesToPauseIs(true))
                .Then(x => ThenTheConsoleWillPrint("timer halted"))
                .BDDfy();
        }

        [Test]
        public void ItShouldPrintTImerResumedWhenTheGameStateChangesToPaused()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .When(x => WhenTheGameStateChangesToPauseIs(false))
                .Then(x => ThenTheConsoleWillPrint("timer resumed"))
                .BDDfy();
        }
    }
}