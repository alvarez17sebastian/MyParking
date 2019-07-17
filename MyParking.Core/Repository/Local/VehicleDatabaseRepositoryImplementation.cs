using System;
using System.Collections.Generic;
using System.Linq;
using MyParking.core.Dto;
using MyParking.Core.CustomExceptions;
using MyParking.Core.Mapping;

namespace MyParking.Core.Repository.Local
{
    public class VehicleDatabaseRepositoryImplementation:IVehicleDatabaseRepository
    {
        private readonly RealmDatabaseManager realmDatabaseManager;

        public VehicleDatabaseRepositoryImplementation()
        {
            realmDatabaseManager = new RealmDatabaseManager();
        }

        #region Behaviors methods

        public List<VehicleDto> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>(realmDatabaseManager.GetRealmInstance().All<Vehicle>());
            return VehicleMapping.ListVehicleToListVehicleDto(vehicles);
        }

        public List<VehicleDto> GetVehicles(string plate)
        {
            List<Vehicle> vehicles = new List<Vehicle>(realmDatabaseManager.GetRealmInstance().All<Vehicle>());
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Plate.Contains(plate)).ToList();
            return VehicleMapping.ListVehicleToListVehicleDto(vehicles);
        }

        public List<VehicleDto> GetVehiclesForType(string typeVehicle)
        {
            List<Vehicle> vehicles = new List<Vehicle>(realmDatabaseManager.GetRealmInstance().All<Vehicle>());
            vehicles = vehicles.Where(currentVehicle => currentVehicle.Type.Equals(typeVehicle)).ToList();
            return VehicleMapping.ListVehicleToListVehicleDto(vehicles);

        }

        public bool SaveVehicle(VehicleDto vehicleDto)
        {
            try
            {
                realmDatabaseManager.GetRealmInstance().Write(() =>
                {
                    Vehicle vehicle = VehicleMapping.VehicleDtoToVehicle(vehicleDto); 
                    realmDatabaseManager.GetRealmInstance().Add(vehicle);
                });
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
            return true;
        }

        public bool UpdateVehicle(VehicleDto vehicleDto)
        {
            //TODO: Empty method.
            return false;
        }

        public bool DeleteVehicle(VehicleDto vehicleDto)
        {
            Vehicle vehicle = realmDatabaseManager.GetRealmInstance().Find<Vehicle>(vehicleDto.Plate);
            using (var trans = realmDatabaseManager.GetRealmInstance().BeginWrite())
            {
                realmDatabaseManager.GetRealmInstance().Remove(vehicle);
                trans.Commit();
            }
            return true;
        }

        #endregion
    }
}
