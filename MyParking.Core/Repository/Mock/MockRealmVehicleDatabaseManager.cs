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
            return GetVehiclesDto(vehicles);
        }

        public List<VehicleDto> GetVehicles(string plate)
        {
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Plate.Contains(plate)).ToList();
            return GetVehiclesDto(vehicles);
        }

        public VehicleDto GetVehicle(string plate)
        {
            throw new NotImplementedException();
        }

        public List<VehicleDto> GetVehiclesForType(string typeVehicle)
        {
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Type.Equals(typeVehicle)).ToList();
            return GetVehiclesDto(vehicles);
        }

        public bool SaveVehicle(VehicleDto vehicleDto)
        {
            Vehicle vehicle = GetVehicleEntity(vehicleDto);
            vehicles.Add(vehicle);
            return true;
        }

        public bool UpdateVehicle(VehicleDto vehicleDto)
        {
            return false;
        }

        public bool DeleteVehicle(VehicleDto vehicleDto)
        {
            Vehicle vehicle = GetVehicleEntity(vehicleDto);
            vehicles.Remove(vehicle);
            return true;
        }

        #endregion

        #region Mapping methods

        private Vehicle GetVehicleEntity(VehicleDto vehicleDto)
        {
            Vehicle vehicle = new Vehicle(vehicleDto.Plate, vehicleDto.Type, vehicleDto.Displacement, vehicleDto.DateOfEntry);
            return vehicle;
        }

        private VehicleDto GetVehicleDto(Vehicle vehicle)
        {
            VehicleDto vehicleDto = new VehicleDto(vehicle.Plate, vehicle.Type, vehicle.Displacement, vehicle.DateOfEntry);
            return vehicleDto;
        }

        private List<VehicleDto> GetVehiclesDto(List<Vehicle> vehicles)
        {
            List<VehicleDto> vehiclesDto = new List<VehicleDto>();
            foreach (var vehicleEntity in vehicles)
            {
                VehicleDto vehicleDto = GetVehicleDto(vehicleEntity);
                vehiclesDto.Add(vehicleDto);
            }
            return vehiclesDto;
        }

        #endregion

    }
}
