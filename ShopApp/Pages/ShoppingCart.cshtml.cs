using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopService.Services;
using ShopService.ViewModels;

namespace ShopAppUI.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly BuyService _buyService;
        public List<ItemViewModel> items { get; set; }
        public Dictionary<int, int> shoppingCart { get; set; } = new Dictionary<int, int>();
        public decimal Amount { get; set; }
        public List<string> Message { get; set; } = new List<string>();
        public ShoppingCartModel(ShopItemsService shopItemsService, ShoppingCartService shoppingCartService, BuyService buyService)
        {
            _shopItemsService = shopItemsService;
            _shoppingCartService = shoppingCartService;
            _buyService = buyService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            shoppingCart = _shoppingCartService.ShoppingCart;
            Amount = _shopItemsService.GetShoppingCartAmount(_shoppingCartService.ShoppingCart);
            items = await _shopItemsService.GetShoppingCartItems(_shoppingCartService.ShoppingCart);
            if (items == null)
                return NotFound();
            else
            {
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await  _buyService.BuyShopItems();
            Message = _buyService.Message;
            Amount = _buyService.Amount;
            return  RedirectToPage("/Index", new { Message, Amount });
        }
        public IActionResult OnPostDelete(int id)
        {
            _shoppingCartService.DeleteItemFromShoppingCart(id);
            return RedirectToPage();
        }
    }
}
