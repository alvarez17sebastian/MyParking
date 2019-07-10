using System;
using System.Collections.Generic;
using MyParking.core.Dto;

namespace MyParking.Core.Repository
{
    public interface IVehicleDatabaseRepository
    {
        //Behaviors methods
        List<VehicleDto> GetAllVehicles();

        List<VehicleDto> GetVehicles(string plate);

        List<VehicleDto> GetVehiclesForType(string typeVehicle);

        bool SaveVehicle(VehicleDto vehicleDto);

        bool UpdateVehicle(VehicleDto vehicleDto);

        bool DeleteVehicle(VehicleDto vehicleDto);

    }
}
