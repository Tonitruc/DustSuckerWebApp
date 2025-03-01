using AutoMapper;
using DustSuckerWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.ViewModels
{
    [AutoMap(typeof(Hoover))]
    /// <summary>
    /// DTO for adding a new vacuum cleaner.
    /// </summary>
    public class AddHooverDto
    {
        /// <summary> Brand of the vacuum cleaner. </summary>
        public string Brand { get; set; }

        /// <summary> Model of the vacuum cleaner. </summary>
        public string Model { get; set; }

        /// <summary> Type of vacuum cleaner. Available values: "Классический", "Автомобильный", "Робот-пылесос", "Вертикальный", "Промышленный". </summary>
        public string Type { get; set; }

        /// <summary> Type of dust bag. Available values: "Бумажный", "Синтетический", "Многоразовый", "Антибактериальный". </summary>
        public string DustBagType { get; set; }

        /// <summary> Cleaning type. Available values: "Сухая", "Моющая", "Аквафильтр". </summary>
        public string CleaningType { get; set; }

        /// <summary> Type of vacuum cleaner tube. Available values: "Цельная", "Составная", "Телескопическая". </summary>
        public string TubeType { get; set; }

        /// <summary> Capacity of the dust bag (in liters). </summary>
        public double DustBagCapacity { get; set; }

        /// <summary> Type of filter. Available values: "Пылесборник", "Моторный". </summary>
        public string FilterType { get; set; }

        /// <summary> Power consumption (in watts). </summary>
        public double PowerConsumption { get; set; }

        /// <summary> Type of power source. Available values: "Аккумулятор", "Источник питания", "Комбинированный". </summary>
        public string PowerType { get; set; }

        /// <summary> Battery capacity (in mAh). </summary>
        public double BatteryCapacity { get; set; }

        /// <summary> Battery life (in minutes). </summary>
        public double BatteryLife { get; set; }

        /// <summary> Cable length (in meters). </summary>
        public double CableLength { get; set; }

        /// <summary> Suction power (in watts). </summary>
        public double SuctionPower { get; set; }

        /// <summary> Included nozzles. Available values: "Для сухой уборки", "Для влажной уборки", "Для ковров", "Для ровных покрытий", "Для шерсти", "Строительные насадки". </summary>
        public List<string> NozzlesIncluded { get; set; }

        /// <summary> Weight of the vacuum cleaner (in kg). </summary>
        public double Weight { get; set; }

        public AddHooverDto()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Type = string.Empty;
            DustBagType = string.Empty;
            CleaningType = string.Empty;
            TubeType = string.Empty;
            FilterType = string.Empty;
            PowerType = string.Empty;
            NozzlesIncluded = [];
        }
    }
}