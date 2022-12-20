using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ShoppingCenter.Models
{
    public class Shop
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? ShopName { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        public string? ShopCategory { get; set; }
        [Range(-1, 3)]
        [Required]
        public int? Floor { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeOnly OpenHour { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeOnly CloseHour { get; set; }
        [RegularExpression(@"Yes|No$")]
        [Required]
        public string? OpenInSunday { get; set; }
    }
    public class ShopGenreViewModel
    {
        public List<Shop>? Shops { get; set; }
        public SelectList? Categories { get; set; }
        public SelectList? Floors { get; set; }
        public string? ShopGenre { get; set; }
        public string? SearchString { get; set; }
        public string? ShopFloor { get; set; }
    }
}