using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Parking.Droid.ListVehicles;
using Android.Content;
using MyParking.Droid;
using MyParking.Droid.DependencyInjection;
using MyParking.Core.DomainModels;
using MyParking.core.Dto;
using MyParking.Core.Constants;
using MyParking.Droid.Views.VehiclesList.PatternCommand;

namespace Parking.Droid
{
    [Activity(Label = "Listado de vehículos", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ListVehiclesActivity : AppCompatActivity
    {
        #region Statement of user interface

        private RecyclerView rvVehicles;
        private FloatingActionButton fabRegisterVehicle;
        private VehiclesAdapter vehiclesAdapter;

        #endregion

        #region Statement of business logic class

        private ParkingLot parking;
        #endregion

        #region Lifecycle

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            parking = ServiceLocatorApp.Get<ParkingLot>();

            InitUserInterface();
            InitAdapter();
            SetupRecyclerView();

        }

        protected override void OnResume()
        {
            base.OnResume();
            LoadVehicles();
        }

        #endregion

        #region Initializations

        private void InitUserInterface()
        {
            rvVehicles = FindViewById<RecyclerView>(Resource.Id.recyclerView_vehicles);
            fabRegisterVehicle = FindViewById<FloatingActionButton>(Resource.Id.floatButton_vehicleAdd);
            fabRegisterVehicle.Click += FabRegisterVehicle_Click;
        }

        private void InitAdapter()
        {
            vehiclesAdapter = new VehiclesAdapter(AdapterActions);
        }

        #endregion

        #region Adapter and recyclerview implementation

        private void SetupRecyclerView()
        {
            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
            rvVehicles.SetLayoutManager(linearLayoutManager);
            rvVehicles.SetAdapter(vehiclesAdapter);
        }

        #endregion

        #region Adapter Actions

        public void AdapterActions(int action, VehicleDto vehicleDto)
        {
            Invoker invoker = new Invoker(this,parking,vehicleDto,vehiclesAdapter);
            invoker.Execute(action);
        }

        #endregion

        #region Button events

        private void FabRegisterVehicle_Click(object sender, System.EventArgs e)
        {
            ChangeActivity();
        }

        #endregion

        #region Actions on the UI

        private void LoadVehicles()
        {
            var listVehicles = parking.GetAllVehicles();
            vehiclesAdapter.AddList(listVehicles);
        }

        #endregion

        #region Change of activities

        private void ChangeActivity()
        {
            Intent intent = new Intent(this, typeof(VehicleRegisterActivity));
            StartActivity(intent);
        }

        #endregion

    }
}