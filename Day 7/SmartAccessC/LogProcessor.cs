namespace SmartAccessC
{
    internal class LogProcessor
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your GateCode, UserInitial, AccessLevel, IsActive and Attempts");
            string userInput = Console.ReadLine();
            string[] parts = userInput.Split('|');

            string code = parts[0];
            char initial = char.Parse(parts[1]);
            byte level = byte.Parse(parts[2]);
            bool activeStatus = bool.Parse(parts[3]);
            byte attemptCount = byte.Parse(parts[4]);
            string accessStatus;

            // Inline validation
            bool isInvalid = code.Length != 2 || !char.IsLetter(code[0]) || !char.IsDigit(code[1]) ||
                             !char.IsUpper(initial) ||
                             level > 7 || level < 0 ||
                             attemptCount > 200 || attemptCount < 0;

            if (isInvalid)
            {
                Console.WriteLine("INVALID ACCESS LOG");
                return;
            }

            if (!activeStatus)
            {
                accessStatus = "ACCESS DENIED - INACTIVE USER";
            }
            else if (attemptCount > 100)
            {
                accessStatus = "ACCESS DENIED - TOO MANY ATTEMPTS";
            }
            else if (level >= 5)
            {
                accessStatus = "ACCESS GRANTED - HIGH SECURITY";
            }
            else
            {
                accessStatus = "ACCESS GRANTED - STANDARD";
            }

            Console.WriteLine($"Gate     : {code,-10}");
            Console.WriteLine($"User     : {initial,-10}");
            Console.WriteLine($"Level    : {level,-10}");
            Console.WriteLine($"Attempts : {attemptCount,-10}");
            Console.WriteLine($"Status   : {accessStatus,-10}");
        }
    }
}
