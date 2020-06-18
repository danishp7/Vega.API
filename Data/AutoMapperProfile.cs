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
        }
    }
}
