using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        

        SortedDictionary<double, string> leaderboard = new SortedDictionary<double, string>();
        leaderboard.Add(55.42, "SwiftRacer");
        leaderboard.Add(52.10, "SpeedDemon");
        leaderboard.Add(58.91, "SteadyEddie");
        leaderboard.Add(51.05, "TurboTom");

        Console.WriteLine(leaderboard.Keys.First());
        leaderboard.Remove(58.91);

        leaderboard.Add(54.00, "SteadyEddie");

        foreach(var item in leaderboard)
        {
            Console.WriteLine($"Key: {item.Key,5} & Value: {item.Value,11}");
        }





        Hashtable ht = new Hashtable();
        ht.Add(101, "Alice");
        ht.Add(102, "Bob");
        ht.Add(103, "charlie");
        ht.Add(104, "Diana");
        if (!ht.ContainsKey(105))
        {
            ht.Add(105, "Edward");
        }
        else
        {
            Console.WriteLine("Key 105 already exists");
        }
        foreach (DictionaryEntry item in ht)
        {
            Console.WriteLine(item.Key + " : " + item.Value);
        }

        string empName = (string)ht[102];
        Console.WriteLine($"102 employee name is {empName}");
        ht.Remove(103);
        Console.WriteLine(ht.Count);
        Console.WriteLine("after deletion");
        foreach (DictionaryEntry item in ht)
        {
            Console.WriteLine(item.Key + " : " + item.Value);
        }
    }
}