using System.Collections.Generic;
using AutoMapper;
using MyParking.core.Dto;

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
