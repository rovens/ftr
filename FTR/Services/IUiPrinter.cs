using System;

namespace FTR.Services
{
    public interface IUiPrinter
    {
        void DisplayNumberHistoryFrequency(TimeSpan timeSpan);
        void OnFibonacciNumber(object sender, EventArgs eventArgs);
        void OnGameStateChange(object sender, GameStateEventArgs gameStateEventArgs);
        void DisplayGoodbye();
        void DisplayPause();
        void DisplayRequestForSeriesOutput();
        void DisplayResumed();
    }
}