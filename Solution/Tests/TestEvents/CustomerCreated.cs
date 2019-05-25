using System;
using System.Collections.Generic;

namespace Tests.TestEvents
{
    public sealed class CustomerCreated : CustomerEvent
    {
        public string Name { get; }
        public string Address { get; }

        public CustomerCreated(Guid aggregateRootId, string name, string address) : base(aggregateRootId)
        {
            Name = name;
            Address = address;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return Name;
            yield return Address;
        }
    }
}