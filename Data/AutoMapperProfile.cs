using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Dtos;
using Vega.API.Models;

namespace Vega.API.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Manufacturer, ManufacturerDto>().ReverseMap();
            CreateMap<Model, ModelDto>().ReverseMap();
            
            CreateMap<VehicleDto, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore());
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(v => v.Accessories, opt => opt.Ignore());
            
            CreateMap<Accessory, AccessoryDto>();
            CreateMap<AccessoryDto, Accessory>()
                .ForMember(a => a.Id, opt => opt.Ignore());

            CreateMap<VehicleAccessory, VehicleAccessoryDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, ReturnUserDto>();
        }
    }
}
