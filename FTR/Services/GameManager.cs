using System;

namespace FTR.Services
{
    public class GameManager : IGameManager
    {

        private readonly IFibonacciInspector _fibonacciInspector;
        public event EventHandler<EventArgs> OnFibonacciNumber;

      
        private readonly IUiPrinter _printer;


        public GameManager(IUiPrinter printer, IFibonacciInspector fibonacciInspector)
        {
            
            _printer = printer;
            _fibonacciInspector = fibonacciInspector;
            OnFibonacciNumber += printer.OnFibonacciNumber;
        }

        

        public void StartGame()
        {

            while (true)
            {
                var input = Console.ReadLine();

                CheckForGameCommand(input);
                int number;
                if (int.TryParse(input, out number))
                {
                    if(_fibonacciInspector.IsFibonacci(number))
                    {
                        OnFibonacciNumber(this, EventArgs.Empty);
                    }

                }
            

                
            }
           
        }

        private void CheckForGameCommand(string input)
        {
            switch (input.ToLower())
            {
                case "quit":
                    _printer.DisplayGoodbye();
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                default:
                    break;

            }
        }
    }
}