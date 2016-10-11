using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FTR.Services
{
    public class FibonacciInspector : IFibonacciInspector
    {
        private readonly IEnumerable<BigInteger> _fibonacciSequence;

        public FibonacciInspector()
        {
            _fibonacciSequence = Fibonacci(1000);
        }


        public bool IsFibonacci(BigInteger newNumber)
        {
            return _fibonacciSequence.Any(x => x == newNumber);
        }

        public IEnumerable<BigInteger> Fibonacci(int n)
        {
            IList<BigInteger> sequence = new List<BigInteger>();
            BigInteger first = 0, second = 1;
            int c;
            for (c = 0; c < n; c++)
            {
                BigInteger next;
                if (c <= 1)
                    next = c;
                else
                {
                    next = first + second;
                    first = second;
                    second = next;
                }
                sequence.Add(next);
            }
            return sequence;
        }
    }
}