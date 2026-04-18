namespace VoltGearSystem.Models
{
    public class MongoDBSetting
    {
        public string? ConnectionString { get; set; } 
        public string? DatabaseName { get; set; }
        public string? CollectionName { get; set; }
    }
}