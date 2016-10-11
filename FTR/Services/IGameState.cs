using System.Collections.Generic;
using System.Numerics;

namespace FTR.Services
{
    public interface IGameState
    {
        SortedList<BigInteger, int> Entries { get; set; }
        void UpdateState(BigInteger number);
    }
}