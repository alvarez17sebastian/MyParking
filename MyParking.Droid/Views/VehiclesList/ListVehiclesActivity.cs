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
using MyParking.Core.Constants;

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
        private VehicleDto vehicleDto = null;

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

        #region Activity behaviors

        private void FabRegisterVehicle_Click(object sender, System.EventArgs e)
        {
            ChangeActivity();
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
                case (int)ActionCodes.actionRegisterCheckout:
                    int payment = parking.CalculatePaymentVehicle(vehicleDto);
                    this.vehicleDto = vehicleDto;
                    CustomDialog customDialog = new CustomDialog();
                    customDialog.ShowCustomDialogInformationPayment(this, payment.ToString(), ConfirmCheckout,null);
                    break;
                case (int)ActionCodes.actionEditVehicle:
                    Toast.MakeText(this, MessageConstants.NotAvailableEdit, ToastLength.Short).Show();
                    break;
                case (int)ActionCodes.actionDeleteVehicle:
                    DeleteVehicle(vehicleDto);
                    break;
                default:
                    Toast.MakeText(this, "Acción no registrada", ToastLength.Short).Show();
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
            {
                Toast.MakeText(this, "Error al eliminar vehiculo", ToastLength.Short).Show();
            }
        }

        private void ChangeActivity()
        {
            Intent intent = new Intent(this, typeof(VehicleRegisterActivity));
            StartActivity(intent);
        }

        #endregion
    }
}