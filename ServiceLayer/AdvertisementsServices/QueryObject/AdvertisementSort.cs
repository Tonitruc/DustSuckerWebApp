using DustSuckerWebApp.Extensions;
using DustSuckerWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.ServiceLayer.AdvertisementsServices.QueryObject
{
    public enum SortedBy : byte
    {
        [Display(Name = "costAscending")] CostAscending,
        [Display(Name = "costDescanding")] CostDescending,
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
