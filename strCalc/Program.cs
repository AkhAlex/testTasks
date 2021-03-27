using System;

namespace strCalculator
{
    class Program
    {
        static void Main(string[] args)
        {   
            string expression = string.Empty;
            Console.WriteLine("Type 'exit' to close application.\n");
            while(true){
                Console.Write("Enter expression: ");
                expression = Console.ReadLine();
                if(expression == "exit")
                    break;
                double result = strCalc.Calculate(expression);
                Console.WriteLine($"answer: {result}");
            }
        }
    }
}
