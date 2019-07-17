using System.Collections.Generic;
using AutoMapper;
using MyParking.core.Dto;

namespace MyParking.Core.Mapping
{
    public static class VehicleMapping
    {
        public static Vehicle VehicleDtoToVehicle(VehicleDto vehicleDto)
        {
            return Mapper.Map<VehicleDto, Vehicle>(vehicleDto);
        }

        public static VehicleDto VehicleToVehicleDto(Vehicle vehicle)
        {
            return Mapper.Map<Vehicle, VehicleDto>(vehicle);
        }

        public static List<Vehicle> ListVehicleDtoToListVehicle(List<VehicleDto> vehiclesDtoList)
        {
            return Mapper.Map<List<VehicleDto>, List<Vehicle>>(vehiclesDtoList);
        }

        public static List<VehicleDto> ListVehicleToListVehicleDto(List<Vehicle> vehiclesList)
        {
            return Mapper.Map<List<Vehicle>, List<VehicleDto>>(vehiclesList);
        }
    }
}
