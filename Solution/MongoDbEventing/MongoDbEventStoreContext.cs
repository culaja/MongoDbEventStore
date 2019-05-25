using MongoDB.Driver;

namespace MongoDbEventing
{
    public sealed class MongoDbEventStoreContext
    {
        private readonly string _collectionName;
        private readonly IMongoDatabase _database;

        public MongoDbEventStoreContext(
            string connectionString, 
            string databaseName,
            string collectionName)
        {
            _collectionName = collectionName;
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<PersistedEvent> GetEventStoreCollection()
            => _database.GetCollection<PersistedEvent>(_collectionName);
    }
}