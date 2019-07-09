using System;
namespace Parking.core.Dto
{
    public class VehicleDto
    {
        public string Plate { set; get; }
        public string Type { set; get; }
        public int Displacement { set; get; }
        public DateTimeOffset DateOfEntry { set; get; }

        public VehicleDto(string licensePlate, string type, int displacement, DateTimeOffset dateOfEntry)
        {
            this.Plate = licensePlate;
            this.Type = type;
            this.Displacement = displacement;
            this.DateOfEntry = dateOfEntry;
        }
    }
}
