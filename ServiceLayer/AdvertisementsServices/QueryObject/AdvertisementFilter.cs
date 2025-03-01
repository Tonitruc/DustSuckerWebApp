using DustSuckerWebApp.Models;

namespace DustSuckerWebApp.ServiceLayer.AdvertisementsServices.QueryObject
{
    public static class AdvertisementFilter
    {
        public static IQueryable<Advertisement> FilterAdvertisementsBy(this IQueryable<Advertisement> query,
            AdvertisementFilterParameters queryParams)
        {
            if (queryParams.Brand != null)
                query = query.Where(ad => ad.Hoover.Brand == queryParams.Brand);

            if(queryParams.MinCost != null)
                query = query.Where(ad => ad.Cost >= queryParams.MinCost);

            if (queryParams.MaxCost != null)
                query = query.Where(ad => ad.Cost <= queryParams.MaxCost);

            if (queryParams.HooverType != null)
                query = query.Where(ad => ad.Hoover.Type == queryParams.HooverType);

            if (queryParams.PowerType != null)
                query = query.Where(ad => ad.Hoover.PowerType == queryParams.PowerType);

            if (queryParams.CleaningType != null)
                query = query.Where(ad => ad.Hoover.CleaningType == queryParams.CleaningType);

            return query;
        }
    }
}
