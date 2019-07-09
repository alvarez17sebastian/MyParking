using System;
using Ninject.Modules;
using Parking.core.Repository.Mock;
using Parking.Core.Repository;

namespace MyParking.Core.DependencyInjection
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
#if DEBUG
            this.Bind<IVehicleDatabaseRepository>().To<MockRealmVehicleDatabaseManager>().InSingletonScope();

#elif RELEASE
            this.Bind<IVehicleDatabaseRepository>().To<RealmVehicleDatabaseManager>().InSingletonScope();
#endif
        }
    }
}
