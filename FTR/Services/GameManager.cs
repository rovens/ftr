using System;
using System.Numerics;

namespace FTR.Services
{
    public class GameManager : IGameManager
    {
        private readonly IFibonacciInspector _fibonacciInspector;
        private readonly IGameState _gameState;
        private readonly IUiPrinter _printer;
        private bool _paused;


        public GameManager(IUiPrinter printer, IFibonacciInspector fibonacciInspector, IGameState gameState)
        {
            _printer = printer;
            _fibonacciInspector = fibonacciInspector;
            _gameState = gameState;

            OnFibonacciNumber += printer.OnFibonacciNumber;
            OnGameStateChanged += printer.OnGameStateChange;
            _paused = false;
        }

        public event EventHandler<EventArgs> OnFibonacciNumber;

        public void StartGame()
        {
            CaptureSeriesPrintInterval();
            while (true)
            {
                var input = Console.ReadLine();

                CheckForGameCommand(input);
                BigInteger number;
                if (BigInteger.TryParse(input, out number) && !_paused)
                {
                    if (_fibonacciInspector.IsFibonacci(number) && !_paused)
                    {
                        OnFibonacciNumber(this, EventArgs.Empty);
                    }
                    _gameState.UpdateState(number);
                }
            }
        }

        public event EventHandler<GameStateEventArgs> OnGameStateChanged;


        public void CaptureSeriesPrintInterval()
        {
            _printer.DisplayRequestForSeriesOutput();
            var interval = Console.ReadLine();
            var secs = 0;
            if (int.TryParse(interval, out secs))
            {
                _printer.DisplayNumberHistoryFrequency(new TimeSpan(0, 0, secs));
            }
            else
            {
                CaptureSeriesPrintInterval();
            }
        }

        private void CheckForGameCommand(string input)
        {
            switch (input.ToLower())
            {
                case "quit":
                    OnGameStateChanged(this, new GameStateEventArgs {IsPaused = true});
                    _printer.DisplayGoodbye();
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                case "pause":
                    if (_paused)
                    {
                        return;
                    }
                    OnGameStateChanged(this, new GameStateEventArgs {IsPaused = true});
                    _paused = true;

                    break;
                case "resume":
                    if (!_paused)
                    {
                        return;
                    }
                    OnGameStateChanged(this, new GameStateEventArgs {IsPaused = false});
                    _paused = false;

                    break;
                default:
                    break;
            }
        }
    }
}