using MyParking.core.Repository.Mock;
using MyParking.Core.Repository;
using MyParking.Core.Repository.Local;
using Ninject.Modules;

namespace MyParking.Core.DependencyInjection
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
#if DEBUG
            this.Bind<IVehicleDatabaseRepository>().To<MockRealmVehicleDatabaseManager>().InSingletonScope();

#elif RELEASE
            this.Bind<IVehicleDatabaseRepository>().To<MockRealmVehicleDatabaseManager>().InSingletonScope();
#endif
        }
    }
}
