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
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        private readonly CategoriesService _categoriesService;       
        [BindProperty]
        public CategoryViewModel CategoryViewModel { get; set; }
        public EditModel(CategoriesService categoriesService)
        {           
            _categoriesService = categoriesService;
        }
        public async Task OnGetAsync(int id)
        {
            CategoryViewModel = await _categoriesService.GetCategoryById(id);
            Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                CategoryViewModel = await _categoriesService.GetCategoryById(id);
                return Page();
            }
            CategoryViewModel.Id = id;
            await _categoriesService.UpdateCategory(CategoryViewModel);
            Message = _categoriesService.Message;
            return RedirectToPage("/Categories/Index", new {Message});
        }
    }
}
