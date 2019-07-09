
using System;
using Android.App;
using Android.Runtime;
using MyParking.Core.DependencyInjection;
using MyParking.Droid.DependencyInjection;

namespace MyParking.Droid
{
    [Application]
    public class MyParkingApplication : Application
    {
        public MyParkingApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            ServiceLocator.SetupKernel();
            ServiceLocatorApp.SetupKernel();

        }
    }
}
