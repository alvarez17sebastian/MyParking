﻿using System;
using System.Collections.Generic;
using System.Linq;
using Parking.core.Dto;
using Parking.Core;
using Parking.Core.Repository;

namespace Parking.core.Repository.Mock
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

        #region Methods for convert Entitys to Dto and Dto to Entity

        public Vehicle GetVehicleEntity(VehicleDto vehicleDto)
        {
            Vehicle vehicle = new Vehicle(vehicleDto.Plate, vehicleDto.Type, vehicleDto.Displacement, vehicleDto.DateOfEntry);
            return vehicle;
        }

        public VehicleDto GetVehicleDto(Vehicle vehicle)
        {
            VehicleDto vehicleDto = new VehicleDto(vehicle.Plate, vehicle.Type, vehicle.Displacement, vehicle.DateOfEntry);
            return vehicleDto;
        }

        public List<VehicleDto> GetVehiclesDto(List<Vehicle> vehicles)
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
