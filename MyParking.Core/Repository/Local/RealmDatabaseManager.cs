using Realms;

namespace MyParking.Core.Repository.Local
{
    public class RealmDatabaseManager:BaseLocalDatabaseManager
    {
        private Realm realmDatabase;

        public RealmDatabaseManager() => SetupLocalDatabase();

        protected override void SetupLocalDatabase()
        {
            RealmConfiguration realmConfiguration = new RealmConfiguration
            {
                SchemaVersion = (ulong)this.databaseVersion
            };
            realmDatabase = Realm.GetInstance(realmConfiguration);
        }

        public Realm GetRealmInstance()
        {
            return realmDatabase;
        }
    }
}
