using MyParking.core.Repository.Mock;
using MyParking.Core.Repository;
using Ninject.Modules;

namespace MyParking.Core.DependencyInjection
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
#if DEBUG
            this.Bind<IVehicleDatabaseRepository>().To<MockRealmVehicleDatabaseManager>().InSingletonScope();
            //this.Bind<IVehicleDatabaseRepository>().To<RealmVehicleDatabaseManager>().InSingletonScope();

#elif RELEASE
            this.Bind<IVehicleDatabaseRepository>().To<RealmVehicleDatabaseManager>().InSingletonScope();
#endif
        }
    }
}
