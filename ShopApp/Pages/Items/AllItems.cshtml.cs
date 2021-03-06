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
        public ShopItems Items { get; set; } = new ShopItems();
        public AllItemsModel(ShopItemsService shopItemsService)
        {
            _shopItemsService = shopItemsService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Items.Items = await _shopItemsService.GetShopItems();
            if (Items.Items == null)
                return NotFound();
            else
            {
                return Page();
            }
        }
        public class ShopItems
        {
            public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
        }
    }
}
