using System;
using System.Linq;
using System.Timers;

namespace FTR.Services
{
//    public interface IOutputWriter
//    {
//        void WriteLine(string text);
//    }
//
//    public class OutputWriter : IOutputWriter
//    {
//        public void WriteLine(string text)
//        {
//         Console.WriteLine(text);   
//        }
//    }

    public class GameStateEventArgs : EventArgs
    {
        public bool IsPaused { get; set; }
    }

    public class UiPrinter : IUiPrinter
    {
        private readonly IGameState _gameState;
        private readonly IOutputWriter _outputWriter;
        private Timer _printSeriesTimer;

        public UiPrinter(IOutputWriter outputWriter, IGameState gameState)
        {
            _outputWriter = outputWriter;
            _gameState = gameState;
        }

        public void DisplayPause()
        {
            _outputWriter.WriteLine("timer halted");
        }

        public void DisplayRequestForSeriesOutput()
        {
            _outputWriter.WriteLine(
                "Please input the number of time in seconds between emitting numbers and their frequency");
        }

        public void OnFibonacciNumber(object sender, EventArgs eventArgs)
        {
            _outputWriter.WriteLine("FIB");
        }

        public void OnGameStateChange(object sender, GameStateEventArgs gameStateEventArgs)
        {
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
            _outputWriter.WriteLine("timer resumed");
        }

        private void DisplaySeries(object sender, ElapsedEventArgs e)
        {
            var items = _gameState.Entries.Select(keyValuePair => $"{keyValuePair.Key}:{keyValuePair.Value}").ToList();
            _outputWriter.WriteLine(string.Join(", ", items));
        }
    }
}