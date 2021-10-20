using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopService.Services;
using ShopService.ViewModels;

namespace ShopAppUI.Pages
{   
    public class BuyItemModel : PageModel
    {     
        private readonly ShopItemsService _shopItemsService;
        private readonly BuyService _buyService;
        private readonly ShoppingCartService _shoppingCartService;
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; } = "";       
        public ItemViewModel Item { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        [Display (Name = "Number of items to buy")]
        [Range(1, 5000)]
        [BindProperty (SupportsGet =true)]
        public int Number { get; set; }
        public BuyItemModel(ShopItemsService shopItemsService, BuyService buyService, ShoppingCartService shoppingCartService)
        {
            _shopItemsService = shopItemsService;
            _buyService = buyService;
            _shoppingCartService = shoppingCartService;
        }      
        public async Task OnGetAsync(int id)
        {
            Item = await _shopItemsService.GetShopItemById(id);
        }
        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("BuyItem", new { id, Message="Please enter a valid number" });
            }
            if(await _buyService.IsItemNumberValid(id, Number))
            {
                _shoppingCartService.AddItem(id, Number);
                Message = _shoppingCartService.Message;
            }
            else
            {
                Message = _buyService.Message[0];
            }
            return RedirectToPage("BuyItem", new {id, Message});
        }
    }
}
