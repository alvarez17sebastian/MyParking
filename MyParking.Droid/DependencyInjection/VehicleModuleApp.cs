using Ninject.Modules;
using Parking.Core.DomainModels;

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
