using MyParking.Core.DomainModels;
using Ninject.Modules;

namespace MyParking.Droid.DependencyInjection
{
    public class VehicleModuleApp : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ParkingLot>().ToSelf().InSingletonScope();
        }
    }
}
