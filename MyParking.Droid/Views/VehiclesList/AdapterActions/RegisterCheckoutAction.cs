using System;
using Android.Content;
using Android.Widget;
using MyParking.core.Dto;
using MyParking.Core.Constants;
using MyParking.Core.DomainModels;
using Parking.Droid.ListVehicles;

namespace MyParking.Droid.Views.VehiclesList.PatternCommand
{
    public class RegisterCheckoutAction:ICommand
    {
        private readonly Context context;
        private readonly ParkingLot parking;
        private readonly VehicleDto vehicleDto;
        private readonly VehiclesAdapter vehiclesAdapter;

        public RegisterCheckoutAction(Context context,ParkingLot parkingLot,VehicleDto vehicleDto,VehiclesAdapter vehiclesAdapter)
        {
            this.context = context;
            this.parking = parkingLot;
            this.vehicleDto = vehicleDto;
            this.vehiclesAdapter = vehiclesAdapter;
        }

        private void ConfirmCheckout()
        {
            DeleteVehicle(this.vehicleDto);
        }

        private void DeleteVehicle(VehicleDto vehicleDtoParam)
        {
            if (parking.DeleteVehicle(vehicleDtoParam))
            {
                Toast.MakeText(context, MessageConstants.DeletedVehicle, ToastLength.Short).Show();
                vehiclesAdapter.Delete(vehicleDtoParam);
            }
            else
            {
                Toast.MakeText(context, MessageConstants.ErrorDeleteVehicle, ToastLength.Short).Show();
            }
        }

        public void Execute()
        {
            int payment = parking.CalculatePaymentVehicle(this.vehicleDto);
            CustomDialog customDialog = new CustomDialog();
            customDialog.ShowCustomDialogInformationPayment((Android.App.Activity)context, payment.ToString(), ConfirmCheckout, null);

        }
    }
}
