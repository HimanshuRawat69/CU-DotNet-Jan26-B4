using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VoltGearSystem.Models;

namespace VoltGearSystem.Services
{
    public class LaptopService
    {

        private readonly IMongoCollection<Laptop> _laptopsCollection;

        public LaptopService(IOptions<MongoDBSetting> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _laptopsCollection = mongoDatabase.GetCollection<Laptop>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<Laptop>> GetAsync() =>
            await _laptopsCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Laptop newLaptop) =>
            await _laptopsCollection.InsertOneAsync(newLaptop);
    }
}
