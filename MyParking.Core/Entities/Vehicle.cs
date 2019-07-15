using System;
using Realms;

namespace MyParking.Core
{
    public class Vehicle : RealmObject
    {
        [PrimaryKey]
        public string Plate { set; get; }

        public string Type { set; get; }

        public int Displacement { set; get; }

        public DateTimeOffset DateOfEntry { set; get; }

        public Vehicle()
        {
        }

        public Vehicle(string licensePlate, string type, int displacement, DateTimeOffset dateOfEntry)
        {
            this.Plate = licensePlate;
            this.Type = type;
            this.Displacement = displacement;
            this.DateOfEntry = dateOfEntry;
        }
    }
}
