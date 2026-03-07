namespace DailyLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"../../../journal.txt";
            //using FileStream fs = new FileStream(fileName, FileMode.Create);
            using StreamWriter sw = new StreamWriter(fileName, true);
            do
            {
                Console.WriteLine("Enter Daily Reflection: ");
                string data = Console.ReadLine();
                if(data == "stop")
                {
                    break;
                }
                sw.WriteLine(data);
            } while (true);
        }
    }
}
