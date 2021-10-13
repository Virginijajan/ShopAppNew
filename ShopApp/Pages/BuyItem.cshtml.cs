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
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; } = "";
        
        public ItemViewModel Item { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        [Display (Name = "Number of items to buy")]
        [Range(0, 5000)]
        [BindProperty (SupportsGet =true)]
        public int Number { get; set; }
        public BuyItemModel(ShopItemsService shopItemsService, BuyService buyService)
        {
            _shopItemsService = shopItemsService;
            _buyService = buyService;
        }      
        public async Task OnGetAsync(int id)
        {
            Item = await _shopItemsService.GetShopItemById(id);
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _buyService.BuyShopItem(id, Number);
            Message = _buyService.Message;
            return RedirectToPage("BuyItem", new {id, Message});          
        }
    }
}
