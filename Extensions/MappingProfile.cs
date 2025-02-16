using AutoMapper;
using DustSuckerWebApp.Models;
using DustSuckerWebApp.ViewModels;

namespace DustSuckerWebApp.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddHooverDto, Hoover>();
        }
    }
}
