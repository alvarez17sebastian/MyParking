using System;
namespace MyParking.Core.CustomExceptions
{
    public class ParkingDomainBusinessException:Exception
    {
        public ParkingDomainBusinessException(string errorMessage):base(errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}
