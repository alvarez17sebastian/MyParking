using Android.App;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Parking.Droid.ListVehicles;
using static Android.Views.View;
using Android.Views;
using Android.Content;
using Android.Widget;
using MyParking.Droid;
using Parking.core.Dto;
using MyParking.Droid.DependencyInjection;
using Parking.Core.DomainModels;

namespace Parking.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ListVehiclesActivity : AppCompatActivity,IOnClickListener
    {
        #region Statement of user interface

        private RecyclerView rvVehicles;
        private FloatingActionButton fabRegisterVehicle;

        #endregion

        private VehiclesAdapter vehiclesAdapter;

        #region Statement of business logic class

        ParkingLot parking;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            parking = ServiceLocatorApp.Get<ParkingLot>();

            InitUserInterface();
            InitAdapter();
            SetEventsListener();
            SetupRecyclerView();

        }

        protected override void OnResume()
        {
            base.OnResume();
            LoadVehicles();
        }

        private void InitUserInterface()
        {
            rvVehicles = FindViewById<RecyclerView>(Resource.Id.recyclerView_vehicles);
            fabRegisterVehicle = FindViewById<FloatingActionButton>(Resource.Id.floatButton_vehicleAdd);
        }

        private void SetEventsListener()
        {
            fabRegisterVehicle.SetOnClickListener(this);
        }

        private void InitAdapter()
        {
            vehiclesAdapter = new VehiclesAdapter(AdapterActions);
        }

        private void SetupRecyclerView()
        {
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
            rvVehicles.SetLayoutManager(linearLayoutManager);
            rvVehicles.SetAdapter(vehiclesAdapter);
        }

        private void LoadVehicles()
        {
            var listVehicles = parking.GetAllVehicles();
            vehiclesAdapter.AddList(listVehicles);
        }

        public void ClickItem(int action, int position, VehicleDto vehicle)
        {
            switch (action)
            {
                case ActionsCode.ACTION_REGISTER_CHECKOUT:
                    //int payment = parking.CalculatePaymentVehicle(vehicle);
                    //parking.RegisterCheckOutVehicle(vehicle);
                    //vehiclesAdapter.Delete(vehicle);
                    //Toast.MakeText(this, payment.ToString(), ToastLength.Short).Show();
                    break;
            }
        }

        public void AdapterActions(int action,VehicleDto vehicleDto)
        {
            switch (action)
            {
                case ActionsCode.ACTION_REGISTER_CHECKOUT:
                    //int payment = parking.CalculatePaymentVehicle(null);
                    Toast.MakeText(this, "Checkout", ToastLength.Short).Show();
                    break;
                case ActionsCode.ACTION_EDIT_VEHICLE:
                    Toast.MakeText(this, "Acción de editar no disponible temporalmente", ToastLength.Short).Show();
                    break;
                case ActionsCode.ACTION_DELETE_VEHICLE:
                    Toast.MakeText(this, "Acción de borrado no disponible temporalmente.", ToastLength.Short).Show();
                    break;
                default:
                    break;
            }
        }

        private void ChangeActivity()
        {
            Intent intent = new Intent(this, typeof(VehicleRegisterActivity));
            StartActivity(intent);
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.floatButton_vehicleAdd:
                    ChangeActivity();
                    break;
                default:
                    break;
            }
        }
    }
}