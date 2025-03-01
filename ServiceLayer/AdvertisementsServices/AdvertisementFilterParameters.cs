namespace DustSuckerWebApp.ServiceLayer.AdvertisementsServices
{
    /// <summary>
    /// Defines the filtering options for advertisements.
    /// </summary>
    public class AdvertisementFilterParameters
    {
        /// <summary> Filter by brand (e.g., "Dyson", "Samsung"). </summary>
        public string? Brand { get; set; }

        /// <summary> Minimum price in the filter range. </summary>
        public decimal? MinCost { get; set; }

        /// <summary> Maximum price in the filter range. </summary>
        public decimal? MaxCost { get; set; }

        /// <summary> Type of vacuum cleaner. Available values: "Классический", "Автомобильный", "Робот-пылесос", "Вертикальный", "Промышленный". </summary>
        public string? HooverType { get; set; }

        /// <summary> Power source type. Available values: "Аккумулятор", "Источник питания", "Комбинированный". </summary>
        public string? PowerType { get; set; }

        /// <summary> Cleaning type. Available values: "Сухая", "Моющая", "Аквафильтр". </summary>
        public string? CleaningType { get; set; }
    }

}
