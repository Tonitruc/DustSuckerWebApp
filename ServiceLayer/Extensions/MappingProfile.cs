using AutoMapper;
using DataLayer.Models;
using ViewModels.ViewModels;
using System.Globalization;

namespace DustSuckerWebApi.Extensions
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
                            src.ImageUrls.FirstOrDefault()))
                        .ForMember(dest => dest.AmountReviews, opt => opt.MapFrom(src => src.Hoover.Reviews.Count))
                        .ForMember(dest => dest.Rating, opt => opt.MapFrom(src 
                            => src.Hoover.Reviews.Count == 0 ? 0: src.Hoover.Reviews.Select(r => r.Rating).Average()));

            CreateMap<Hoover, HooverShortDto>()
                        .ForMember(dest => dest.AmountReviews, opt => opt.MapFrom(src => src.Reviews.Count))
                        .ForMember(dest => dest.Rating, opt => opt.MapFrom(src
                            => src.Reviews.Count == 0 ? 0 : src.Reviews.Select(r => r.Rating).Average()));

            CreateMap<Advertisement, AdvertisementDto>()
                        .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src =>
                            src.PublishDate.ToString("dd.MM.yyyy HH:mm")))
                        .ForMember(dest => dest.Hoover, opt => opt.MapFrom(src => src.Hoover));

            CreateMap<AddUserDto, User>();

            CreateMap<AddReviewDto, Review>();

            CreateMap<AddUserDto, User>();

            CreateMap<Hoover, HooverDto>()
                .ForMember(dest => dest.AmountReviews, opt => opt.MapFrom(src => src.Reviews.Count))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src
                    => src.Reviews.Count == 0 ? 0 : src.Reviews.Select(r => r.Rating).Average()));
        }
    }
}
