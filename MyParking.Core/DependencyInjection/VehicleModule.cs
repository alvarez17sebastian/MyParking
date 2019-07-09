using System;
using Ninject.Modules;
using Parking.Core.Repository;

namespace MyParking.Core.DependencyInjection
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IVehicleDatabaseRepository>().To<RealmVehicleDatabaseManager>().InSingletonScope();
        }
    }
}
