using System;
using System.Runtime.Serialization;

namespace MyParking.Core.CustomExceptions
{
    public class DatabaseException:Exception
    {
        public DatabaseException():base()
        {

        }

        public DatabaseException(string errorMessage, Exception exception ) 
            : base(errorMessage,exception)
        {

        }

        public DatabaseException(SerializationInfo serializationInfo,
                                  StreamingContext streamingContext) :
                                   base(serializationInfo, streamingContext)
        {

        }

        public DatabaseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
