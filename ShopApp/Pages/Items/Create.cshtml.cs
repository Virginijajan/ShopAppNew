using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopService.ViewModels;

namespace ShopAppUI.Pages.Items
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        private readonly CategoriesService _categoriesService;
        [BindProperty]
        public CreateItemModel Item { get; set; } = new CreateItemModel();
     
        public CreateModel(ShopItemsService shopItemsService, CategoriesService categoriesService)
        {
            _shopItemsService = shopItemsService;
            _categoriesService = categoriesService;
        }
        public async Task OnGetAsync()
        {           
            var categories = await _categoriesService.GetCategories();
            foreach (var category in categories)           
                Item.Categories = Item.Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });            
            Item.Images=GetImages.GetImagesNames();
            Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoriesService.GetCategories();
                foreach (var category in categories)
                    Item.Categories = Item.Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
                Item.Images=GetImages.GetImagesNames();
                return Page();
            }           
            await _shopItemsService.AddNewShopItemDb(Item.itemViewModel);
            return RedirectToPage("/Items/Index", new {_shopItemsService.Message });
        }
        public class CreateItemModel
        {          
            public ItemViewModel itemViewModel { get; set; }
            public CategoryViewModel categoryViewModel { get; set; }
            public string Message { get; set; }
            public IEnumerable<SelectListItem> Categories { get; set; }
                = new List<SelectListItem>();
            public IEnumerable<SelectListItem> Images { get; set; }
                = new List<SelectListItem>();
        }
    }
}
