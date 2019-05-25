using System.Collections.Generic;

namespace Common
{
    public interface IEventStore
    {
        IReadOnlyList<IDomainEvent> Append(IReadOnlyList<IDomainEvent> domainEvent);

        IEnumerable<IReadOnlyList<IDomainEvent>> LoadAll(int batchSize = 10000);
    }
}