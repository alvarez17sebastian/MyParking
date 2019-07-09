using System;
using AutoMapper;

namespace MyParking.Core.Mapping
{
    public class MapperService
    {
        public static void InitAutomapper()
        {
            Mapper.Initialize(value => value.AddProfile<MapProfile>());
        }
    }
}
