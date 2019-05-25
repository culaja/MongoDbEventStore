using System;
using Common;

namespace Tests.TestEvents
{
    public abstract class CustomerEvent : DomainEvent
    {
        protected CustomerEvent(Guid aggregateRootId) : base(aggregateRootId, "Customer")
        {
        }
    }
}