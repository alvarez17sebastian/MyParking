using System;
using System.Runtime.Serialization;

namespace MyParking.Core.CustomExceptions
{
    public class ParkingDomainBusinessException:Exception
    {

        public ParkingDomainBusinessException()
        {
        }

        public ParkingDomainBusinessException(string errorMessage, Exception exception)
            : base(errorMessage, exception)
        {

        }

        public ParkingDomainBusinessException(SerializationInfo serializationInfo,
                                              StreamingContext streamingContext) :
                                              base(serializationInfo, streamingContext)
        {
        }

        public ParkingDomainBusinessException(string errorMessage):base(errorMessage)
        {
        }
    }
}
