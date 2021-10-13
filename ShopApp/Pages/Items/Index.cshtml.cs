using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopService.ViewModels;

namespace ShopAppUI.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        [BindProperty (SupportsGet =true)]
        public string Message { get; set; }
        public List<ItemViewModel> itemsViewModel { get; set; } = new List<ItemViewModel>();
        public IndexModel(ShopItemsService shopItemsService)
        {
            _shopItemsService = shopItemsService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            itemsViewModel = await _shopItemsService.GetShopItems();

            if (itemsViewModel == null)
                return NotFound();
            else
                return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _shopItemsService.DeleteShopItem(id);
            return RedirectToPage("/Items/Index", new { Message=_shopItemsService.Message});   
        }
    }
}
