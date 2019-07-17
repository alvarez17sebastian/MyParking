using System;
using AutoMapper;

namespace MyParking.Core.Mapping
{
    public static class MapperService
    {
        public static void InitAutomapper()
        {
            Mapper.Initialize(value => value.AddProfile<MapProfile>());
        }
    }
}
