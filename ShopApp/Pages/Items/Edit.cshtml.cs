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
using static ShopAppUI.Pages.Items.CreateModel;

namespace ShopAppUI.Pages.Items
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ShopItemsService _shopItemsService;
        private readonly CategoriesService _categoriesService;

        [BindProperty]
        public CreateItemModel Item { get; set; } = new CreateItemModel();
        public EditModel(ShopItemsService shopItemsService, CategoriesService categoriesService)
            {
                _shopItemsService = shopItemsService;
                _categoriesService = categoriesService;
            }       
        public async Task OnGetAsync(int id)
        {
            Item.itemViewModel= await _shopItemsService.GetShopItemById(id);
            var categories = await _categoriesService.GetCategories();
            foreach(var category in categories)
                Item.Categories=Item.Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
            Item.Images=GetImages.GetImagesNames();
            Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Item.itemViewModel = await _shopItemsService.GetShopItemById(id);
                var categories = await _categoriesService.GetCategories();
                foreach (var category in categories)
                    Item.Categories = Item.Categories.Append(new SelectListItem { Value = category.Id.ToString(), Text = category.Name });
                Item.Images=GetImages.GetImagesNames();
                return Page();
            }
            Item.itemViewModel.Id = id;            
            await _shopItemsService.UpdateShopItemDb(Item.itemViewModel);
            Item.Message= _shopItemsService.Message;
            return RedirectToPage("/Items/Index", new {Item.Message});
        }
    }
}
