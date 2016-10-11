using System;

namespace FTR.Services
{
    public class OutputWriter : IOutputWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}