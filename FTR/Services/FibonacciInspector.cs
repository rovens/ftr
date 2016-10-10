using System.Collections.Generic;
using System.Linq;

namespace FTR.Services
{
    public class FibonacciInspector : IFibonacciInspector
    {
        private IEnumerable<int> _fibonacciSequence;

        public FibonacciInspector()
        {
            _fibonacciSequence = Fibonacci(100);
        }


        public bool IsFibonacci(int newNumber)
        {
            return _fibonacciSequence.Any(x => x == newNumber);
        }

        private static IEnumerable<int> Fibonacci(int n)
        {
            IList<int> sequence = new List<int>();
            int first = 0, second = 1;
            int c;
            for (c = 0; c < n; c++)
            {
                int next;
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