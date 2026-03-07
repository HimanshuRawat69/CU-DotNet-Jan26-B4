namespace DisplayLine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisLine();
            DisLine('$');
            DisLine(ch: '+', length: 60);
        }
        static void DisLine(char ch = '-', int length = 40)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(ch);
            }
            Console.WriteLine();
        }
    }
}
