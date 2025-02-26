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

            CreateMap<Advertisement, AdvertisementShortDto>()
                        .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src =>
                            src.PublishDate.ToString("dd.MM.yyyy HH:mm")))
                        .ForMember(dest => dest.HooverId, opt => opt.MapFrom(src =>
                            src.HooverId))
                        .ForMember(dest => dest.MainImageUrl, opt => opt.MapFrom(src =>
                            src.ImageUrls.FirstOrDefault()));

            CreateMap<Hoover, HooverShortDto>();
            CreateMap<Advertisement, AdvertisementDto>()
                        .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src =>
                            src.PublishDate.ToString("dd.MM.yyyy HH:mm")))
                        .ForMember(dest => dest.Hoover, opt => opt.MapFrom(src => src.Hoover));

            CreateMap<AddUserDto, User>();

        }
    }
}
