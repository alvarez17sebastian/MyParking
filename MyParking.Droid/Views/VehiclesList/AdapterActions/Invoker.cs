using System;
using System.Collections.Generic;
using Android.Content;
using MyParking.core.Dto;
using MyParking.Core.DomainModels;
using Parking.Droid;
using Parking.Droid.ListVehicles;

namespace MyParking.Droid.Views.VehiclesList.PatternCommand
{
    public class Invoker
    {
        private readonly Dictionary<int, ICommand> actions = new Dictionary<int, ICommand>();

        private readonly Context context;
        private readonly ParkingLot parking;
        private readonly VehicleDto vehicleDto;
        private readonly VehiclesAdapter vehiclesAdapter;

        public Invoker(Context context, ParkingLot parkingLot, VehicleDto vehicleDto, VehiclesAdapter vehiclesAdapter)
        {
            this.context = context;
            this.parking = parkingLot;
            this.vehicleDto = vehicleDto;
            this.vehiclesAdapter = vehiclesAdapter;
            RegisterActions();
        }

        private void RegisterActions()
        {
            actions.Add((int)ActionCodes.actionRegisterCheckout, new RegisterCheckoutAction(context,parking,vehicleDto,vehiclesAdapter));
            actions.Add((int)ActionCodes.actionDeleteVehicle, new DeleteAction(context, parking, vehicleDto, vehiclesAdapter));
            actions.Add((int)ActionCodes.actionEditVehicle, new EditAction(context));
        }

        public void Execute(int key)
        {
            if (actions[key] != null)
            {
                actions[key].Execute();
            }
        }
    }
}
