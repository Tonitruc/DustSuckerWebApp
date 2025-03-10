using ServiceLayer.AdvertisementsServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels.ViewModels;

namespace DustSuckerWebApp.Pages.Catalog
{
    public class IndexModel : PageModel
    {
        private readonly AdvertisementService _advertisementService;


        #region Pages properties

        public List<AdvertisementDto> Advertisements { get; set; }

        #endregion


        public IndexModel(AdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;

            Advertisements = [];
        }


        public async void OnGetAsync()
        {
            Advertisements = await _advertisementService.GetAsync();
        }
    }
}
