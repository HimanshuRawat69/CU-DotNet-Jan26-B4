namespace CricketPerfromance
{
    internal class Program
    {
        class Player
        {
            public string Name { get; set; }
            public int RunsScored { get; set; }
            public int BallsFaced { get; set; }
            public bool IsOut { get; set; }
            public double StrikeRate { get; set; }
            public double Average { get; set; }

            public void Calculation()
            {
                if (BallsFaced == 0) StrikeRate = 0;
                else
                {
                    StrikeRate = (double)(RunsScored / BallsFaced) * 100;

                }

                if (!IsOut) { Average = RunsScored; }
                else { Average = RunsScored; }
            }
        }
        static void Main(string[] args)
        {
            string file = @"..\..\..\players.csv";
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                do
                {
                    Console.WriteLine("Enter file data");
                    string data = Console.ReadLine();
                    if (data == "stop")
                    {
                        break;
                    }
                    sw.WriteLine(data);
                } while (true);
            }
            Console.Write("Enter path to players.csv file: ");
            string filePath = Console.ReadLine();

            List<Player> players = new List<Player>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    try
                    {
                        string[] parts = line.Split(',');

                        string name = parts[0].Trim();
                        int runs = int.Parse(parts[1].Trim());
                        int balls = int.Parse(parts[2].Trim());
                        bool isOut = bool.Parse(parts[3].Trim());

                        Player player = new Player
                        {
                            Name = name,
                            RunsScored = runs,
                            BallsFaced = balls,
                            IsOut = isOut
                        };

                        player.Calculation();
                        players.Add(player);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($" Invalid data format skipped: {line}");
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine($" Divide by zero issue skipped: {line}");
                    }
                }

                var filteredPlayers = players.Where(p => p.BallsFaced >= 10).OrderByDescending(p => p.StrikeRate).ToList();

                Console.WriteLine();
                Console.WriteLine("Name            Runs    SR      Avg");
                Console.WriteLine("---------------------------------------");

                foreach (var p in filteredPlayers)
                {
                    Console.WriteLine($"{p.Name,-15} {p.RunsScored,-7} {p.StrikeRate,6:f2} {p.Average,6:f2}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(" Error: CSV file not found. Please check the file path.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Unexpected error: {ex.Message}");
            }
        }
    }
}
