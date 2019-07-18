using System;
using Android.Content;
using Android.Widget;
using MyParking.core.Dto;
using MyParking.Core.Constants;
using MyParking.Core.DomainModels;
using Parking.Droid.ListVehicles;

namespace MyParking.Droid.Views.VehiclesList.PatternCommand
{
    public class DeleteAction:ICommand
    {
        private readonly Context context;
        private readonly ParkingLot parking;
        private readonly VehicleDto vehicleDto;
        private readonly VehiclesAdapter vehiclesAdapter;

        public DeleteAction(Context context,ParkingLot parkingLot,VehicleDto vehicleDto, VehiclesAdapter vehiclesAdapter)
        {
            this.context = context;
            this.parking = parkingLot;
            this.vehicleDto = vehicleDto;
            this.vehiclesAdapter = vehiclesAdapter;
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
            DeleteVehicle(this.vehicleDto);
        }
    }
}
