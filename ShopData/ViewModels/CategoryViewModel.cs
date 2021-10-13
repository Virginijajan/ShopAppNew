using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength (30)]
        [Display (Name ="Category name")]
        public string Name { get; set; }
        public List<ItemViewModel> ItemViewModels { get; set; }
    }
}
