using AutoMapper;
using DustSuckerWebApp.Models;
using DustSuckerWebApp.ViewModels;
using System.Globalization;

namespace DustSuckerWebApp.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddHooverDto, Hoover>();

            CreateMap<AddAdvertisementDto, Advertisement>()
                        .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src =>
                            DateTime.ParseExact(src.PublishDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture)))
                        .ForMember(dest => dest.Hoover, opt => opt.MapFrom(src => src.Hoover));

            CreateMap<Advertisement, ShortAdvertisementDto>()
                        .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src =>
                            src.PublishDate.ToString("dd.MM.yyyy HH:mm")))
                        .ForMember(dest => dest.HooverId, opt => opt.MapFrom(src => 
                        src.HooverId));

            CreateMap<AddUserDto, User>();

        }
    }
}
