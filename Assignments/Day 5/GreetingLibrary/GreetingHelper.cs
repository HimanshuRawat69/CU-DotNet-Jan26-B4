namespace GreetingLibrary
{
    public class GreetingHelper
    {
        public static string GetGreeting(string name) 
        {
            if (name == "")
            {
                return "Hello, Guest!";
            }
            return $"Hello, {name}";
        }
    }
}
