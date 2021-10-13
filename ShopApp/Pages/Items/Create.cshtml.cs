using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopService.ViewModels;

namespace ShopAppUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        private readonly CategoriesService _categoriesService;
        
        [BindProperty]
        public ItemViewModel itemViewModel { get; set; }       
        public CategoryViewModel categoryViewModel { get; set; }
        public string Message { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
            = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Images { get; set; }
            = new List<SelectListItem>();
        public CreateModel(ShopItemsService shopItemsService, CategoriesService categoriesService)
        {
            _shopItemsService = shopItemsService;
            _categoriesService = categoriesService;
        }
        public async Task OnGetAsync()
        {           
            var categories = await _categoriesService.GetCategories();
            foreach (var category in categories)           
                Categories = Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });            
            Images=GetImages.GetImagesNames();
            Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoriesService.GetCategories();
                foreach (var category in categories)
                    Categories = Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
                Images=GetImages.GetImagesNames();
                return Page();
            }           
            await _shopItemsService.AddNewShopItemDb(itemViewModel);
            return RedirectToPage("/Items/Index", new {_shopItemsService.Message });
        }
    }
}