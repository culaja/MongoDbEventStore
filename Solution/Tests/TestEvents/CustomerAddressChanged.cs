using System;
using System.Collections.Generic;

namespace Tests.TestEvents
{
    public sealed class CustomerAddressChanged : CustomerEvent
    {
        public string OldAddress { get; }
        public string NewAddress { get; }

        public CustomerAddressChanged(Guid aggregateRootId, string oldAddress, string newAddress) : base(aggregateRootId)
        {
            OldAddress = oldAddress;
            NewAddress = newAddress;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return OldAddress;
            yield return NewAddress;
        }
    }
}