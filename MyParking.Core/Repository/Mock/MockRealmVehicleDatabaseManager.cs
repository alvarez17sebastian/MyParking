using System;
using System.Collections.Generic;
using System.Linq;
using MyParking.core.Dto;
using MyParking.Core;
using MyParking.Core.Mapping;
using MyParking.Core.Repository;

namespace MyParking.core.Repository.Mock
{
    public class MockRealmVehicleDatabaseManager : IVehicleDatabaseRepository
    {
        private List<Vehicle> vehicles = new List<Vehicle>();

        #region Behaviors methods

        public List<VehicleDto> GetAllVehicles()
        {
            return VehicleMapping.ListVehicleToListVehicleDto(vehicles);
        }

        public List<VehicleDto> GetVehicles(string plate)
        {
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Plate.Contains(plate)).ToList();
            return VehicleMapping.ListVehicleToListVehicleDto(vehicles);
        }

        public VehicleDto GetVehicle(string plate)
        {
            throw new NotImplementedException();
        }

        public List<VehicleDto> GetVehiclesForType(string typeVehicle)
        {
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Type.Equals(typeVehicle)).ToList();
            return VehicleMapping.ListVehicleToListVehicleDto(vehicles);
        }

        public bool SaveVehicle(VehicleDto vehicleDto)
        {
            Vehicle vehicle = VehicleMapping.VehicleDtoToVehicle(vehicleDto);
            vehicles.Add(vehicle);
            return true;
        }

        public bool UpdateVehicle(VehicleDto vehicleDto)
        {
            return false;
        }

        public bool DeleteVehicle(VehicleDto vehicleDto)
        {
            Vehicle vehicle = VehicleMapping.VehicleDtoToVehicle(vehicleDto);
            vehicles.Remove(vehicle);
            return true;
        }

        #endregion

    }
}
