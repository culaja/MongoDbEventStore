using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using MongoDB.Driver;
using static System.DateTime;
using static MongoDbEventing.MongoDbHelpers;
using static MongoDbEventing.PersistedEvent;
using static MongoDB.Driver.Builders<MongoDbEventing.PersistedEvent>;

namespace MongoDbEventing
{
    public sealed class EventStore : IEventStore
    {
        private readonly IMongoCollection<PersistedEvent> _mongoCollection;

        public EventStore(MongoDbEventStoreContext mongoDbEventStoreContext)
        {
            _mongoCollection = mongoDbEventStoreContext.GetEventStoreCollection();
        }
        
        public IReadOnlyList<IDomainEvent> Append(IReadOnlyList<IDomainEvent> domainEvent)
        {
            var appendTimestamp = UtcNow;
            _mongoCollection.InsertOne(PersistedEventFrom(
                domainEvent.Select(e => 
                    e.SetTimestamp(appendTimestamp)).ToList()));

            return domainEvent;
        }

        public IEnumerable<IReadOnlyList<IDomainEvent>> LoadAll(int batchSize = 10000)
        {
            using (var cursor = _mongoCollection.FindSync(Filter.Empty, FindOptionsFor(batchSize)))
            {
                cursor.MoveNextAsync().Wait();
                Task<bool> cursorTask;
                do
                {
                    var events = cursor.Current;
                    cursorTask = cursor.MoveNextAsync();
                    yield return events.AsParallel().SelectMany(e => e.ToDomainEvents()).ToList();
                } while (cursorTask.Result);
            }
        }
    }
}