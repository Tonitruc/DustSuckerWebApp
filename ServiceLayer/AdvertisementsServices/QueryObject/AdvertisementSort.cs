using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ServiceLayer.Extensions;

namespace ServiceLayer.AdvertisementsServices.QueryObject
{
    public enum SortedBy : byte
    {
        [Display(Name = "CostAscending")] CostAscending,
        [Display(Name = "CostDescanding")] CostDescending,
        [Display(Name = "Rating")] Rating,
        [Display(Name = "PublishDate")] PublishDate
    }

    public static class AdvertisementSort
    {
        public static IQueryable<Advertisement>SortAdvertisementsBy(this IQueryable<Advertisement> query,
            string? sortBy)
        {
            var sortedByEnum = sortBy?.FromDisplayName<SortedBy>();
            return sortedByEnum switch
            {
                SortedBy.CostAscending => query.OrderBy(ad => EF.Functions.Collate(ad.Cost.ToString(), "NOCASE")),
                SortedBy.CostDescending => query.OrderByDescending(ad => EF.Functions.Collate(ad.Cost.ToString(), "NOCASE")),
                SortedBy.Rating => query.OrderByDescending(ad => ad.Hoover.DustBagType),
                SortedBy.PublishDate => query.OrderByDescending(query => query.PublishDate),
                _ => query
            };
               
        }
    }
}
