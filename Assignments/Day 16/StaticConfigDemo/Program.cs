namespace StaticConfigDemo
{
    class ApplicationCofig
    {
        public static string ApplicationName { get; set; }
        public static string Environment { get; set; }
        public static int AccessCount { get; set; }
        public static bool IsInitialized { get; set; }

        static ApplicationCofig()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            AccessCount = 0;
            IsInitialized = false;
            Console.WriteLine("Static constructor executed");
        }
        public static void Initialize(string appName, string environment)
        { 
            ApplicationName = appName;
            Environment = environment;
            IsInitialized=true;
            AccessCount++;
        }

        public static string GetConfigurationSummary()
        {
            AccessCount++;
            return $"Application Name- {ApplicationName} Environment- {Environment} Access Count-{AccessCount} Initialization Status- {IsInitialized}";
        }

        public static void ResetConfiguration()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            IsInitialized = false;
            AccessCount++;
        }
    }
        internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ApplicationCofig.ApplicationName);
            ApplicationCofig.Initialize("ConfigTracker", "Production");
            Console.WriteLine(ApplicationCofig.GetConfigurationSummary());
            ApplicationCofig.ResetConfiguration();
            Console.WriteLine("After Reset");
            Console.WriteLine(ApplicationCofig.GetConfigurationSummary());
        }
    }
}
