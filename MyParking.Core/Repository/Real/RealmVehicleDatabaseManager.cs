﻿using System;
using System.Collections.Generic;
using System.Linq;
using Parking.core.Dto;
using Realms;

namespace Parking.Core.Repository
{
    public class RealmVehicleDatabaseManager:IVehicleDatabaseRepository
    {
        private Realm realmDatabase;

        private const int DatabaseVersion = 1;

        public RealmVehicleDatabaseManager()
        {
            RealmConfiguration realmConfiguration = new RealmConfiguration
            {
                SchemaVersion = DatabaseVersion
            };
            realmDatabase = Realm.GetInstance(realmConfiguration);
        }

        #region Behaviors methods

        public List<VehicleDto> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>(realmDatabase.All<Vehicle>());
            return GetVehiclesDto(vehicles);
        }

        public List<VehicleDto> GetVehicles(string plate)
        {
            List<Vehicle> vehicles = new List<Vehicle>(realmDatabase.All<Vehicle>());
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Plate.Contains(plate)).ToList();
            return GetVehiclesDto(vehicles);
        }

        public List<VehicleDto> GetVehiclesForType(string typeVehicle)
        {
            List<Vehicle> vehicles = new List<Vehicle>(realmDatabase.All<Vehicle>());
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Type.Equals(typeVehicle)).ToList();
            return GetVehiclesDto(vehicles);
        }

        public bool SaveVehicle(VehicleDto vehicleDto)
        {
            realmDatabase.Write(() =>
            {
                Vehicle vehicle = GetVehicleEntity(vehicleDto);
                realmDatabase.Add(vehicle);
            });
            return true;
        }
        
        public bool UpdateVehicle(VehicleDto vehicleDto)
        {
            //TODO: Empty method.
            return false;
        }

        public bool DeleteVehicle(VehicleDto vehicleDto)
        {
            Vehicle vehicle = realmDatabase.Find<Vehicle>(vehicleDto.Plate);
            using (var trans = realmDatabase.BeginWrite())
            {
                realmDatabase.Remove(vehicle);
                trans.Commit();
            }
            return true;
        }

        #endregion

        #region Methods for convert Entitys to Dto and Dto to Entity

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
