using GreetingLibrary;

namespace GreetingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter your name: ");
            string name1= Console.ReadLine();
            string greet = GreetingHelper.GetGreeting(name1);
            Console.WriteLine(greet);
        }
    }
}
