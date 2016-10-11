using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FTR.Services;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace FTR.Tests
{
    [TestFixture(Category = "UnitTest")]
    public class FibonacciInspectorTests
    {



        [SetUp]
        public void SetUp()
        {
            _testFixture = new FibonacciInspector();
        }

        private FibonacciInspector _testFixture;
        private List<BigInteger> _fibonacciSequenceResponse;
        private int _sequenceLength;

        private void ThenEachNumberInTheSequenceShouldBeTheSumOfTheTwoNumberBeforeIt()
        {
            for (var i = 0; i < _sequenceLength; i++)
            {
                if (i < 2)
                {
                }
                else
                {
                    _fibonacciSequenceResponse[i].ShouldBe(_fibonacciSequenceResponse[i - 1] +
                                                           _fibonacciSequenceResponse[i - 2]);
                }
            }
        }

        private void GivenADesiredFibonacciSequenceOf(int sequence)
        {
            _sequenceLength = sequence;
        }


        private void ThenTheSequenceShouldBe(BigInteger[] series)
        {
            _fibonacciSequenceResponse.ShouldBe(series);
        }

        private void WhenFibonacciIsCalled()
        {
            _fibonacciSequenceResponse = _testFixture.Fibonacci(_sequenceLength).ToList();
        }

        [Test]
        public void ANumberInTheSequenceShouldAlwaysBeTheSumOfTheTwoNumbersBeforeIt()
        {
            this
                .Given(x => x.GivenADesiredFibonacciSequenceOf(1000))
                .When(x => x.WhenFibonacciIsCalled())
                .Then(x => x.ThenEachNumberInTheSequenceShouldBeTheSumOfTheTwoNumberBeforeIt())
                .BDDfy();
        }
    }
}