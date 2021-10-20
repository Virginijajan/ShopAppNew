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
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        private readonly CategoriesService _categoriesService;

        [BindProperty]
        public CategoryViewModel categoryViewModel { get; set; }
        public CreateModel(CategoriesService categoriesService)
        {         
            _categoriesService = categoriesService;
        }
        public void OnGet()
        {            
            Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _categoriesService.AddNewCategory(categoryViewModel);
            Message = _categoriesService.Message;
            return RedirectToPage("/Categories/Index", new {Message});
        }        
    }
}
