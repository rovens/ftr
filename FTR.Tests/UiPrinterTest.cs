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
            _gameState = new GameState();
            _testFixture = new UiPrinter(_mockConsole, _gameState);
        }

        private IOutput _mockConsole;
        private UiPrinter _testFixture;
        private IGameState _gameState;

        private void GivenAGameHasBeenStarted()
        {
            _testFixture.DisplayNumberHistoryFrequency(TimeSpan.FromSeconds(10));
        }



        private void ThenTheConsoleWillPrint(string text)
        {
            _mockConsole.Received().WriteLine(Arg.Is(text));
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
                .When(x => WhenTheGameStateChangesTo("halt"))

                .Then(x => ThenTheConsoleWillPrint("timer halted"))
           
                .BDDfy();
        }

        [Test]
        public void ItShouldPrintTheResultsAndAGoodbyeMessageOnQutting()
        {
            this
               .Given(x => GivenAGameHasBeenStarted())
               .And(x => AndTheUserHasEntered(1))
               .And(x => AndTheUserHasEntered(1))
               .And(x => AndTheUserHasEntered(2))
               .When(x => WhenTheGameStateChangesTo("quitting"))
               .Then(x => ThenTheConsoleWillPrint("1:2, 2:1"))
               .And(x => ThenTheConsoleWillPrint("Thanks for playing, press any key to exit."))
               .BDDfy();
        }

        private void WhenTheGameStateChangesTo(string state)
        {
            switch (state)
            {
                case "quitting":
                    _testFixture.OnGameStateChange(this, new GameStateEventArgs { IsQuitting = true});
                    break;
                case "halt":
                    _testFixture.OnGameStateChange(this, new GameStateEventArgs { IsPaused = true });
                    break;
                case "resume":
                    _testFixture.OnGameStateChange(this, new GameStateEventArgs { IsQuitting = false });
                    break;

            }
        }

        private void AndTheUserHasEntered(int input)
        {
            _gameState.UpdateState(input);
        }

        [Test]
        public void ItShouldPrintTimerResumedWhenTheGameStateChangesToPaused()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .When(x => WhenTheGameStateChangesTo("resume"))
                .Then(x => ThenTheConsoleWillPrint("timer resumed"))
                .BDDfy();
        }
    }
}