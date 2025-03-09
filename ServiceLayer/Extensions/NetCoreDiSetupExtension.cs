using DustSuckerWebApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.AdvertisementsServices;
using ServiceLayer.HoverServices;
using ServiceLayer.UserServices;

namespace ServiceLayer.Extensions
{
    public static class NetCoreDiSetupExtension
    {
        public static void AddServiceLayerDi(this IServiceCollection services)
        {
            services.AddScoped<HooverService>();
            services.AddScoped<AdvertisementService>();
            services.AddScoped<UserService>();

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
