using System;

namespace FTR.Services
{
    public interface IUiPrinter
    {
        void DisplayNumberHistoryFrequency();
        void Resume();
        void OnFibonacciNumber(object sender, EventArgs eventArgs);
        void DisplayGoodbye();
    }
}