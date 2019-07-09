using System;
using AutoMapper;
using Parking.core.Dto;
using Parking.Core;

namespace MyParking.Core.Mapping
{
    public class MapProfile:Profile
    {
        public void Setup()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleDto, Vehicle>();
        }
    }
}
