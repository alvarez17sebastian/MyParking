using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Parking.Droid.ListVehicles;
using static Android.Views.View;
using Android.Views;
using Android.Content;
using Android.Widget;
using MyParking.Droid;
using MyParking.Droid.DependencyInjection;
using MyParking.Core.DomainModels;
using MyParking.core.Dto;

namespace Parking.Droid
{
    [Activity(Label = "Listado de vehículos", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ListVehiclesActivity : AppCompatActivity, IOnClickListener
    {
        #region Statement of user interface

        private RecyclerView rvVehicles;
        private FloatingActionButton fabRegisterVehicle;
        private VehiclesAdapter vehiclesAdapter;

        #endregion

        #region Statement of business logic class

        private ParkingLot parking;
        private VehicleDto vehicleDto = null;

        #endregion

        #region Lifecycle
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
        #endregion

        #region Initializations
        private void InitUserInterface()
        {
            rvVehicles = FindViewById<RecyclerView>(Resource.Id.recyclerView_vehicles);
            fabRegisterVehicle = FindViewById<FloatingActionButton>(Resource.Id.floatButton_vehicleAdd);
        }

        private void InitAdapter()
        {
            vehiclesAdapter = new VehiclesAdapter(AdapterActions);
        }
        #endregion

        #region Activity behaviors
        private void SetEventsListener()
        {
            fabRegisterVehicle.SetOnClickListener(this);
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

        public void AdapterActions(int action, VehicleDto vehicleDto)
        {
            switch (action)
            {
                case ActionsCode.ACTION_REGISTER_CHECKOUT:
                    int payment = parking.CalculatePaymentVehicle(vehicleDto);
                    this.vehicleDto = vehicleDto;
                    CustomDialog customDialog = new CustomDialog();
                    customDialog.ShowCustomDialogInformationPayment(this, payment.ToString(), ConfirmCheckout);
                    break;
                case ActionsCode.ACTION_EDIT_VEHICLE:
                    Toast.MakeText(this, "Acción de editar no disponible temporalmente", ToastLength.Short).Show();
                    break;
                case ActionsCode.ACTION_DELETE_VEHICLE:
                    DeleteVehicle(vehicleDto);
                    break;
                default:
                    break;
            }
        }

        private void ConfirmCheckout()
        {
            DeleteVehicle(this.vehicleDto);
        }

        private void DeleteVehicle(VehicleDto vehicleDtoParam)
        {
            if (parking.DeleteVehicle(vehicleDtoParam))
            {
                Toast.MakeText(this, "Vehiculo eliminado", ToastLength.Short).Show();
                vehiclesAdapter.Delete(vehicleDtoParam);
            }
            else
                Toast.MakeText(this, "Error al eliminar vehiculo", ToastLength.Short).Show();
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
        #endregion
    }
}