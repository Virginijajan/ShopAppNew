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
        public CategoryViewModel categoryViewModel { get; set; }
        public EditModel(CategoriesService categoriesService)
        {           
            _categoriesService = categoriesService;
        }
        public async Task OnGetAsync(int id)
        {
            categoryViewModel = await _categoriesService.GetCategoryById(id);
            Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                categoryViewModel = await _categoriesService.GetCategoryById(id);
                return Page();
            }
            categoryViewModel.Id = id;
            await _categoriesService.UpdateCategory(categoryViewModel);
            Message = _categoriesService.Message;
            return RedirectToPage("/Categories/Index", new {Message});
        }
    }
}
