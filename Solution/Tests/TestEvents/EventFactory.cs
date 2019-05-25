using System;
using static System.Guid;

namespace Tests.TestEvents
{
    public static class EventFactory
    {
        private static Guid Customer1Id = NewGuid();
        private static string Customer1Name = nameof(Customer1Name);
        private static string Customer1OriginalAddress = nameof(Customer1OriginalAddress);
        private static string Customer1NewAddress = nameof(Customer1NewAddress);
        
        public static CustomerCreated Customer1Created = new CustomerCreated(Customer1Id, Customer1Name, Customer1OriginalAddress).SetVersion(1) as CustomerCreated;
        public static CustomerAddressChanged Customer1AddressChanged = new CustomerAddressChanged(Customer1Id, Customer1OriginalAddress, Customer1NewAddress).SetVersion(1) as CustomerAddressChanged;

        private static Guid Customer2Id = NewGuid();
        private static string Customer2Name = nameof(Customer2Name);
        private static string Customer2OriginalAddress = nameof(Customer2OriginalAddress);
        private static string Customer2NewAddress = nameof(Customer2NewAddress);
        
        public static CustomerCreated Customer2Created = new CustomerCreated(Customer2Id, Customer2Name, Customer2OriginalAddress).SetVersion(1) as CustomerCreated;
        public static CustomerAddressChanged Customer2AddressChanged = new CustomerAddressChanged(Customer2Id, Customer2OriginalAddress, Customer2NewAddress).SetVersion(1) as CustomerAddressChanged;
    }
}