using System;
namespace MyParking.Core.CustomExceptions
{
    public class DatabaseException:Exception
    {
        public DatabaseException(string errorMessage) : base(errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}
