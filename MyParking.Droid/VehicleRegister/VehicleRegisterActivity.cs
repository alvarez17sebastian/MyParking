
using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MyParking.Core.DependencyInjection;
using MyParking.Droid;
using Parking.core.Dto;
using Parking.Core.CustomExceptions;
using Parking.Core.DomainModels;
using static Android.Views.View;

namespace Parking.Droid
{
    [Activity(Label = "RegisterVehicleActivity")]
    public class VehicleRegisterActivity : Activity,IOnClickListener
    {
        #region Statement of user interface
        private Button buttonRegisterVehicle;
        private TextInputEditText textInputEditTextLicensePlate;
        private TextInputEditText textInputEditTextTypeVehicle;
        private TextInputEditText textInputEditTextDisplacement;
        #endregion

        #region Statement of business logic class

        ParkingLot parking;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_vehicle_register);
            InitUserInterface();
            SetEventsListener();
            InitBusinessLogic();

        }

        private void InitUserInterface()
        {
            buttonRegisterVehicle = FindViewById<Button>(Resource.Id.button_RegisterVehicle_activityRegisterVehicle);
            textInputEditTextLicensePlate = FindViewById<TextInputEditText>(Resource.Id.textInputEditText_LicensePlate_activityVehicleRegister);
            textInputEditTextTypeVehicle = FindViewById<TextInputEditText>(Resource.Id.textInputEditText_TypeVehicle_activityVehicleRegister);
            textInputEditTextDisplacement = FindViewById<TextInputEditText>(Resource.Id.textInputEditText_Displacement_activityVehicleRegister);
        }

        private void InitBusinessLogic()
        {
            parking = ServiceLocator.Get<ParkingLot>();
        }

        private void SetEventsListener()
        {
            buttonRegisterVehicle.SetOnClickListener(this);
        }

        private void RegisterVehicle()
        {
            try
            {
                VehicleDto vehicleDto = CreateVehicle();
                parking.RegisterCheckInVehicle(vehicleDto);
            }
            catch (ParkingDomainBusinessException businessException)
            {
                Toast.MakeText(this,businessException.Message,ToastLength.Short).Show();
            }
        }

        private VehicleDto CreateVehicle()
        {
            string plate = textInputEditTextLicensePlate.Text;
            string vehicleType = textInputEditTextTypeVehicle.Text;
            string strDisplacement = textInputEditTextDisplacement.Text;
            int displacement = !InputIsNullOrEmpty(strDisplacement)? int.Parse(strDisplacement):0;
            return new VehicleDto(plate, vehicleType, displacement, DateTime.Now);
        }

        private bool InputIsNullOrEmpty(string strInput)
        {
            return string.IsNullOrEmpty(strInput);
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.button_RegisterVehicle_activityRegisterVehicle:
                    RegisterVehicle();
                    break;
            }
        }
    }
}
