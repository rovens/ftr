using System;
using System.Numerics;

namespace FTR.Services
{
    public class GameManager : IGameManager
    {
        private readonly IFibonacciInspector _fibonacciInspector;
        private readonly IGameState _gameState;
        private readonly IOutput _output;
        private readonly IUiPrinter _printer;
        internal bool Paused;


        public GameManager(IUiPrinter printer, IFibonacciInspector fibonacciInspector, IGameState gameState, IOutput output)
        {
            _printer = printer;
            _fibonacciInspector = fibonacciInspector;
            _gameState = gameState;
            _output = output;

            OnFibonacciNumber += printer.OnFibonacciNumber;
            OnGameStateChanged += printer.OnGameStateChange;
            OnGameStateChanged += GameStateChanged;
            Paused = false;
        }

        private void GameStateChanged(object sender, GameStateEventArgs e)
        {

            if (e.IsQuitting)
            {
                _output.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Paused = e.IsPaused;
            }
        }

        public event EventHandler<EventArgs> OnFibonacciNumber;

        public void StartGame()
        {
            CaptureSeriesPrintInterval();
            while (true)
            {
                var input = _output.ReadLine();

                CheckForGameCommand(input);
                BigInteger number;
                if (BigInteger.TryParse(input, out number) && !Paused)
                {
                    if (_fibonacciInspector.IsFibonacci(number) && !Paused)
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

        internal void CheckForGameCommand(string input)
        {
            switch (input.ToLower())
            {
                case "quit":
                    OnGameStateChanged(this, new GameStateEventArgs {IsQuitting = true});   
                    break;
                case "halt":
                    if (Paused)
                    {
                        return;
                    }
                    OnGameStateChanged(this, new GameStateEventArgs {IsPaused = true});
                    break;
                case "resume":
                    if (!Paused)
                    {
                        return;
                    }
                    OnGameStateChanged(this, new GameStateEventArgs {IsPaused = false});
                    break;
            }
        }
    }
}