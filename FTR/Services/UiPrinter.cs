using System;

namespace FTR.Services
{
    public interface IOutputWriter
    {
        void WriteLine(string text);
    }

    public class OutputWriter : IOutputWriter
    {
        public void WriteLine(string text)
        {
         Console.WriteLine(text);   
        }
    }

    public class UiPrinter : IUiPrinter
    {
        private readonly IOutputWriter _outputWriter;

        public UiPrinter(IOutputWriter outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public void DisplayNumberHistoryFrequency()
        {
            
        }

        public void OnFibonacciNumber(object sender, EventArgs eventArgs)
        {
            _outputWriter.WriteLine("FIB");
        }

        public void DisplayGoodbye()
        {
            Console.WriteLine("Thanks for playing, press any key to exit.");
        }

        public void NotifyUserOfFibonacciNumber()
        {
            
        }

        public void Resume()
        {
            
        }
        public void Halt()
        {
            
        }
    }
}