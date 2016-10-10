using System;

namespace FTR.Services
{
    public interface IGameManager
    {
        void StartGame();
        event EventHandler<EventArgs> OnFibonacciNumber;
    }
}