using DataLayer.Models;

namespace ServiceLayer.AdvertisementsServices.QueryObject
{
    public static class AdvertisementFilter
    {
        public static IQueryable<Advertisement> FilterAdvertisementsBy(this IQueryable<Advertisement> query,
            AdvertisementFilterParameters? queryParams)
        {
            if (queryParams == null) return query;

            if (queryParams.Brand != null)
                query = query.Where(ad => ad.Title.Contains(queryParams.Brand));

            if(queryParams.MinCost != null)
                query = query.Where(ad => ad.Cost >= queryParams.MinCost);

            if (queryParams.MaxCost != null)
                query = query.Where(ad => ad.Cost <= queryParams.MaxCost);

            if (queryParams.HooverType != null)
                query = query.Where(ad => queryParams.HooverType.Contains(ad.Hoover.Type));

            if (queryParams.PowerType != null)
                query = query.Where(ad => queryParams.PowerType.Contains(ad.Hoover.PowerType));

            if (queryParams.CleaningType != null)
                query = query.Where(ad => queryParams.CleaningType.Contains(ad.Hoover.CleaningType));

            return query;
        }
    }
}
