using System;
using GreetingLibrary;

namespace GreetingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            string greeting = GreetingHelper.GetGreeting(name);

            Console.WriteLine(greeting);
            Console.ReadKey();
        }
    }
}
