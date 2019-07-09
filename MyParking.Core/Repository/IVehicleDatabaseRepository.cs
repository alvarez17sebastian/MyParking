using System;
using System.Collections.Generic;
using Parking.core.Dto;

namespace Parking.Core.Repository
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
