using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopService.ViewModels;
using static ShopAppUI.Pages.Items.AllItemsModel;

namespace ShopAppUI.Pages.Items
{
    public class AvailableModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        public ShopItems Items { get; set; } = new ShopItems();
        public AvailableModel(ShopItemsService shopItemsService)
        {
            _shopItemsService = shopItemsService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
           Items.Items = await _shopItemsService.GetAvailableShopItems();
            if (Items.Items == null)
                return NotFound();
            else
            {
                return Page();
            }
        }
    }
}
