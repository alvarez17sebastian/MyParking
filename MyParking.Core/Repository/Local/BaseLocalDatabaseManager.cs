using System;
namespace MyParking.Core.Repository.Local
{
    public abstract class BaseLocalDatabaseManager
    {
        protected int databaseVersion = 2;

        protected abstract void SetupLocalDatabase();
    }
}
