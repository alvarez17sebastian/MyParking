using AutoMapper;
using MyParking.core.Dto;

namespace MyParking.Core.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleDto, Vehicle>();
        }
    }
}
