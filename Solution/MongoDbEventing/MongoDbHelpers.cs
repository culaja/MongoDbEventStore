using MongoDB.Driver;

namespace MongoDbEventing
{
    public static class MongoDbHelpers
    {
        public static FindOptions<PersistedEvent, PersistedEvent> FindOptionsFor(int batchSize) =>
            new FindOptions<PersistedEvent, PersistedEvent>
            {
                BatchSize = batchSize
            };
    }
}