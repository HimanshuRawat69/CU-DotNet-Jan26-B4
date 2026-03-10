namespace StreamBuzz
{
    public class CreatorStats
    {
        public string CreatorName { get; set; }
        public double[] WeeklyLikes { get; set; }
    }

    public class Program
    {
        public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();

        public static void RegisterCreator(CreatorStats record)
        {
            if (!EngagementBoard.Contains(record))
            {
                EngagementBoard.Add(record);
            }
        }

        public static Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
        {
            Dictionary<string, int> res = new Dictionary<string, int>();

            foreach (var r in records)
            {
                int count = r.WeeklyLikes.Count(l => l >= likeThreshold);
                if (count > 0)
                {
                    res[r.CreatorName] = count;
                }
            }

            return res;
        }

        public double CalculateAverageLikes()
        {
            double totalLikes = 0;
            int totalCount = 0;

            foreach (var r in EngagementBoard)
            {
                totalLikes += r.WeeklyLikes.Sum();
                totalCount += r.WeeklyLikes.Length;
            }

            if (totalCount == 0)
                return 0;

            return totalLikes / totalCount;
        }
        static void Main(string[] args)
        {
            Program p = new Program();

            while (true)
            {
                Console.WriteLine("1. Register Creator");
                Console.WriteLine("2. Show Top Posts");
                Console.WriteLine("3. Calculate Average Likes");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Creator Name:");
                        string name = Console.ReadLine();

                        double[] likes = new double[4];
                        Console.WriteLine("Enter weekly likes (Week 1 to 4):");

                        for (int i = 0; i < 4; i++)
                        {
                            likes[i] = double.Parse(Console.ReadLine());
                        }

                        CreatorStats cs = new CreatorStats
                        {
                            CreatorName = name,
                            WeeklyLikes = likes
                        };

                        RegisterCreator(cs);
                        Console.WriteLine("Creator registered successfully");
                        break;

                    case 2:
                        Console.WriteLine("Enter like threshold:");
                        double threshold = double.Parse(Console.ReadLine());

                        var result = GetTopPostCounts(EngagementBoard, threshold);

                        if (result.Count == 0)
                        {
                            Console.WriteLine("No top-performing posts this week");
                        }
                        else
                        {
                            foreach (var item in result)
                            {
                                Console.WriteLine(item.Key + " - " + item.Value);
                            }
                        }
                        break;

                    case 3:
                        double avg = p.CalculateAverageLikes();
                        Console.WriteLine("Overall average weekly likes: " + avg);
                        break;

                    case 4:
                        Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                        return;
                }

            }
        }
    }
}
