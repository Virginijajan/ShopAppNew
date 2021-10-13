using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopService.ViewModels;

namespace ShopAppUI.Pages.Items
{
    public class AllItemsModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        public List<ItemViewModel> items { get; set; }
        public AllItemsModel(ShopItemsService shopItemsService)
        {
            _shopItemsService = shopItemsService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            items = await _shopItemsService.GetShopItems();
            if (items == null)
                return NotFound();
            else
            {
                return Page();
            }
        }
    }
}
