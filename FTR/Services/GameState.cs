using System.Collections.Generic;
using System.Numerics;

namespace FTR.Services
{
    public class GameState : IGameState
    {
        public GameState()
        {
            Entries = new SortedList<BigInteger, int>();
        }

        public SortedList<BigInteger, int> Entries { get; set; }

        public void UpdateState(BigInteger number)
        {
            if (Entries.ContainsKey(number))
            {
                Entries[number]++;
            }
            else
            {
                Entries.Add(number, 1);
            }
        }
    }
}