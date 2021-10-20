using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopService.ViewModels;

namespace ShopAppUI.Pages.Categories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CategoriesService _categoriesService;

        public List<CategoryViewModel> Categories { get; set; }
        [BindProperty (SupportsGet =true)]
        public string Message { get; set; }
        public IndexModel(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        public async Task OnGetAsync()
        {
            Categories = await _categoriesService.GetCategories();
            Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _categoriesService.DeleteCategory(id);
            Message = _categoriesService.Message;
            return RedirectToPage("/Categories/Index", new {Message});
        }
    }
}
