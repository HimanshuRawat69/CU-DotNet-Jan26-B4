namespace Leaderboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<double, string> leaderboard = new SortedDictionary<double, string>();
            leaderboard.Add(55.42, "SwiftRacer");
            leaderboard.Add(52.10, "SpeedDemon");
            leaderboard.Add(58.91, "SteadyEddie");
            leaderboard.Add(51.05, "TurboTom");
            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"{entry.Value} - {entry.Key} seconds");
            }
            var fastestEntry = leaderboard.First(); 
            Console.WriteLine($"Fastest Lap: {fastestEntry.Value} with {fastestEntry.Key} seconds");
            leaderboard.Remove(58.91);          
            leaderboard.Add(54.00, "SteadyEddie"); 

            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"{entry.Value} - {entry.Key} seconds");
            }
        }
        
}
}
