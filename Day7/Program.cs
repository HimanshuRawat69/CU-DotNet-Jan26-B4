using System;

class Program
{
    static void Main()
    {
        string logLine = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(logLine))
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        string[] data = logLine.Split('|');
        if (data.Length != 5)
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        // -------- GateCode Validation --------
        string gate = data[0];
        if (gate.Length != 2)
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        char gateLetter = gate[0];
        char gateDigit = gate[1];

        if (!char.IsLetter(gateLetter) || !char.IsDigit(gateDigit))
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        // -------- User Initial Validation --------
        if (data[1].Length != 1)
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        char userChar = data[1][0];
        if (!char.IsUpper(userChar))
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        // -------- Access Level Validation --------
        if (!byte.TryParse(data[2], out byte accessLevel))
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        if (accessLevel < 1 || accessLevel > 7)
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        // -------- IsActive Validation --------
        if (!bool.TryParse(data[3], out bool active))
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        // -------- Attempts Validation --------
        if (!byte.TryParse(data[4], out byte attemptCount))
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        if (attemptCount > 200)
        {
            Console.WriteLine("INVALID ACCESS LOG");
            return;
        }

        // -------- Business Logic --------
        string result;

        if (!active)
            result = "ACCESS DENIED – INACTIVE USER";
        else if (attemptCount > 100)
            result = "ACCESS DENIED – TOO MANY ATTEMPTS";
        else if (accessLevel >= 5)
            result = "ACCESS GRANTED – HIGH SECURITY";
        else
            result = "ACCESS GRANTED – STANDARD";

        // -------- Formatted Output --------
        Console.WriteLine($"{ "Gate".PadRight(10)}: {gate}");
        Console.WriteLine($"{ "User".PadRight(10)}: {userChar}");
        Console.WriteLine($"{ "Level".PadRight(10)}: {accessLevel}");
        Console.WriteLine($"{ "Attempts".PadRight(10)}: {attemptCount}");
        Console.WriteLine($"{ "Status".PadRight(10)}: {result}");
    }
}
