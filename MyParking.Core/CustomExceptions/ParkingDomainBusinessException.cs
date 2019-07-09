using System;
namespace Parking.Core.CustomExceptions
{
    public class ParkingDomainBusinessException:Exception
    {
        public ParkingDomainBusinessException(string errorMessage):base(errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}
