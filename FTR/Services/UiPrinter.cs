using System;
using System.Linq;
using System.Timers;

namespace FTR.Services
{
//    public interface IOutput
//    {
//        void WriteLine(string text);
//    }
//
//    public class Output : IOutput
//    {
//        public void WriteLine(string text)
//        {
//         Console.WriteLine(text);   
//        }
//    }

    public class GameStateEventArgs : EventArgs
    {
        public GameStateEventArgs()
        {
            IsQuitting = false;
            IsPaused = false;
        }
        public bool IsQuitting { get; set; }
        public bool IsPaused { get; set; }
    }

    public class UiPrinter : IUiPrinter
    {
        private readonly IGameState _gameState;
        private readonly IOutput _output;
        private Timer _printSeriesTimer;

        public UiPrinter(IOutput output, IGameState gameState)
        {
            _output = output;
            _gameState = gameState;
        }

        public void DisplayPause()
        {
            _output.WriteLine("timer halted");
        }

        public void DisplayRequestForSeriesOutput()
        {
            _output.WriteLine(
                "Please input the number of time in seconds between emitting numbers and their frequency");
        }

        public void OnFibonacciNumber(object sender, EventArgs eventArgs)
        {
            _output.WriteLine("FIB");
        }

        public void OnGameStateChange(object sender, GameStateEventArgs gameStateEventArgs)
        {
            if (gameStateEventArgs.IsQuitting)
            {
                DisplayGoodbye();
                return;
            }

            if (_printSeriesTimer == null)
            {
                return;
            }
            if (gameStateEventArgs.IsPaused)
            {
                _printSeriesTimer.Stop();
                DisplayPause();
            }
            else
            {
                _printSeriesTimer.Start();
                DisplayResumed();
            }
        }

        public void DisplayGoodbye()
        {
            Console.WriteLine("Thanks for playing, press any key to exit.");
        }

        public void DisplayNumberHistoryFrequency(TimeSpan timeSpan)
        {
            _printSeriesTimer = new Timer(timeSpan.TotalMilliseconds);
            _printSeriesTimer.Elapsed += DisplaySeries;
            _printSeriesTimer.Enabled = true;
        }

        public void DisplayResumed()
        {
            _output.WriteLine("timer resumed");
        }

        private void DisplaySeries(object sender, ElapsedEventArgs e)
        {
            var items = _gameState.Entries.Select(keyValuePair => $"{keyValuePair.Key}:{keyValuePair.Value}").ToList();
            _output.WriteLine(string.Join(", ", items));
        }
    }
}