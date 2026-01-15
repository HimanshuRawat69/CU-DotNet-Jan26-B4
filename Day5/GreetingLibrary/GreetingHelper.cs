using System;

namespace GreetingLibrary
{
    public class GreetingHelper
    {
        public static string GetGreeting(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Hello, Guest!";
            }
            else
            {
                return "Hello, " + name + "!";
            }
        }
    }
}
