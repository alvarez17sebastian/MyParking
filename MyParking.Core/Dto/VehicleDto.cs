using System;

namespace MyParking.core.Dto
{
    public class VehicleDto
    {
        public string Plate { get; private set; }
        public string Type { get; private set; }
        public int Displacement { get; private set; }
        public DateTimeOffset DateOfEntry { get; private set; }

        public VehicleDto()
        {

        }

        public VehicleDto(string licensePlate, string type, int displacement, DateTimeOffset dateOfEntry)
        {
            this.Plate = licensePlate;
            this.Type = type;
            this.Displacement = displacement;
            this.DateOfEntry = dateOfEntry;
        }
    }
}
