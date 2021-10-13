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
    public class EditModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        private readonly CategoriesService _categoriesService;
        public string Message { get; set; }
        [BindProperty]
        public ItemViewModel itemViewModel { get; set; }       
        public CategoryViewModel categoryViewModel { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
            = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Images { get; set; }
            = new List<SelectListItem>();
        public EditModel(ShopItemsService shopItemsService, CategoriesService categoriesService)
            {
                _shopItemsService = shopItemsService;
                _categoriesService = categoriesService;
            }       
        public async Task OnGetAsync(int id)
        {
            itemViewModel= await _shopItemsService.GetShopItemById(id);
            var categories = await _categoriesService.GetCategories();
            foreach(var category in categories)
                Categories=Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
            Images=GetImages.GetImagesNames();
            Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                itemViewModel = await _shopItemsService.GetShopItemById(id);
                var categories = await _categoriesService.GetCategories();
                foreach (var category in categories)
                    Categories = Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
                Images=GetImages.GetImagesNames();
                return Page();
            }
            itemViewModel.Id = id;            
            await _shopItemsService.UpdateShopItemDb(itemViewModel);
            Message = _shopItemsService.Message;
            return RedirectToPage("/Items/Index", new {Message});
        }
    }
}
