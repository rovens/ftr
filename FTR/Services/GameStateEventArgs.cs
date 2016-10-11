using System;

namespace FTR.Services
{
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
}