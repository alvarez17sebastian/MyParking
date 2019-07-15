
using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MyParking.core.Dto;
using MyParking.Core.Constants;
using MyParking.Core.CustomExceptions;
using MyParking.Core.DependencyInjection;
using MyParking.Core.DomainModels;
using MyParking.Droid;
using static Android.Views.View;

namespace Parking.Droid
{
    [Activity(Label = "Registro de vehículo",Theme = "@style/AppTheme")]
    public class VehicleRegisterActivity : Activity,IOnClickListener
    {
        #region Statement of user interface
        private Button buttonRegisterVehicle;
        private TextInputEditText textInputEditTextLicensePlate;
        private TextInputEditText textInputEditTextDisplacement;
        private Spinner spinnerVehicleTypes;
        #endregion

        #region Statement of business logic class

        ParkingLot parking;

        #endregion

        #region Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_vehicle_register);
            InitUserInterface();
            SetEventsListener();
            InitBusinessLogic();
        }
        #endregion

        #region Initializations
        private void InitUserInterface()
        {
            buttonRegisterVehicle = FindViewById<Button>(Resource.Id.button_RegisterVehicle_activityRegisterVehicle);
            textInputEditTextLicensePlate = FindViewById<TextInputEditText>(Resource.Id.textInputEditText_LicensePlate_activityVehicleRegister);
            textInputEditTextDisplacement = FindViewById<TextInputEditText>(Resource.Id.textInputEditText_Displacement_activityVehicleRegister);
            spinnerVehicleTypes = FindViewById<Spinner>(Resource.Id.spinner_typeVehicle_activityVehicleRegister);
        }

        private void InitBusinessLogic()
        {
            parking = ServiceLocator.Get<ParkingLot>();
        }
        #endregion

        #region Activity behaviors
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
            string vehicleType = GetVehicleTypeKey();
            string strDisplacement = textInputEditTextDisplacement.Text;
            int displacement = !InputIsNullOrEmpty(strDisplacement)? int.Parse(strDisplacement):0;
            return new VehicleDto(plate, vehicleType, displacement, DateTime.Now);
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(textInputEditTextLicensePlate.Text))
            {
                textInputEditTextLicensePlate.Error = MessageConstants.RequiredField;
                return false;
            }else if (string.IsNullOrEmpty(textInputEditTextDisplacement.Text))
            {
                textInputEditTextDisplacement.Error = MessageConstants.RequiredField;
                return false;
            }
            else
            {
                return true;
            }
        }

        private string GetVehicleTypeKey()
        {
            string valueSpinnerVehicleType = spinnerVehicleTypes.SelectedItem.ToString();
            if (valueSpinnerVehicleType.Equals("Carro"))
            {
                return ParkingConstants.CarType;
            }
            else
            {
                return ParkingConstants.MotorBikeType;
            }
        }

        private bool InputIsNullOrEmpty(string strInput)
        {
            return string.IsNullOrEmpty(strInput);
        }

        private void SetEventsListener()
        {
            buttonRegisterVehicle.SetOnClickListener(this);
        }


        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.button_RegisterVehicle_activityRegisterVehicle:
                    if (ValidateFields())
                    {
                        RegisterVehicle();
                        Finish();
                    }
                    break;
            }
        }
        #endregion
    }
}
