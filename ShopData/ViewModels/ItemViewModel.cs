using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength (30)]
        [Display (Name="Shop items name")]
        public string Name { get; set; }
        [Required]
        [Display (Name="Category ID")]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength (300)]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Range (0, 5000)]
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        [Range (0.01, 1000)]
        public decimal Price { get; set; }    
    }
}
