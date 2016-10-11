using FTR.Services;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;
using TestStack.BDDfy;

namespace FTR.Tests
{
    [TestFixture]
    public class GameManagerTests
    {
        private IGameState _gameState;
        private IUiPrinter _uiPrinter;
        private FibonacciInspector _fibonacciInspector;
        private GameManager _testFixture;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _gameState = Substitute.For<IGameState>();
            _fibonacciInspector = new FibonacciInspector();
            _output = Substitute.For<IOutput>();
            _uiPrinter = Substitute.For<IUiPrinter>();
            _testFixture = new GameManager(_uiPrinter, _fibonacciInspector, _gameState, _output);
        }

        [Test]
        public void ItShouldHaltWhenTheUserTypesHalt()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .When(x => WhenTheUserTypesIntoTheConsole("halt"))
                .Then(x => ThenTheGameIsPausedWillBe(true))
                .BDDfy();
        }

        [Test]
        public void ItShouldResumeWhenTheUserTypesResume()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .And(x => GivenTheGameIsHaltedIs(true))
                .When(x => WhenTheUserTypesIntoTheConsole("resume"))
                .Then(x => ThenTheGameIsPausedWillBe(false))
                .BDDfy();
        }

        [Test]
        public void ItShouldResumeThePrintingOfTheSequenceWhenTheUserTypesResume()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .And(x => GivenTheGameIsHaltedIs(true))
                .When(x => WhenTheUserTypesIntoTheConsole("resume"))
                .Then(x => ThenTheUiPrinterWillReceiveTheOnGameStateChangedEventWithIsPaused(false))
                .BDDfy();
        }



        [Test]
        public void ItShouldStopThePrintingOfTheSequenceWhenTheUserTypesHalt()
        {
            this
                .Given(x => GivenAGameHasBeenStarted())
                .And(x => GivenTheGameIsHaltedIs(false))
                .When(x => WhenTheUserTypesIntoTheConsole("halt"))
                .Then(x => ThenTheUiPrinterWillReceiveTheOnGameStateChangedEventWithIsPaused(true))
                .BDDfy();
        }
        private void ThenTheUiPrinterWillReceiveTheOnGameStateChangedEventWithIsPaused(bool expectedIsPaused)
        {
            _uiPrinter.Received().OnGameStateChange(Arg.Any<object>(), Arg.Is<GameStateEventArgs>(x => x.IsPaused == expectedIsPaused));
        }

        private void GivenTheGameIsHaltedIs(bool state)
        {
            _testFixture.Paused = state;
        }

        private void ThenTheGameIsPausedWillBe(bool expectedState)
        {
            _testFixture.Paused.ShouldBe(expectedState);
        }

        private void WhenTheUserTypesIntoTheConsole(string command)
        {
            _testFixture.CheckForGameCommand(command);
        }

        private void GivenAGameHasBeenStarted()
        {
            //_testFixture.StartGame();
        }
    }
}