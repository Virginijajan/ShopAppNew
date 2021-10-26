using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopService.Data;
using ShopService.ViewModels;
using static ShopAppUI.Pages.Items.AllItemsModel;

namespace ShopAppUI.Pages.Items
{
    public class ListModel : PageModel
    {      
        private readonly ShopItemsService _shopItemsService;
        public CategoryViewModel categoryViewModel { get; set; }
        public ShopItems Items { get; set; } = new ShopItems();
        public ListModel(ShopItemsService shopItemsService)
        {          
            _shopItemsService = shopItemsService;           
        }
        public async Task <IActionResult> OnGetAsync(int id)
        {           
            categoryViewModel = await _shopItemsService.GetShopItemsByCategory(id);
            Items.Items.AddRange(categoryViewModel.ItemViewModels);
            if (categoryViewModel == null)
                return NotFound();
            else
            {                
                return Page();
            }
        }        
    }
}
