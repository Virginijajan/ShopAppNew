using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopService.Data;
using ShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopApp.Pages
{
    public class IndexModel : PageModel
    {      
        private readonly CategoriesService _categoriesService;        
        public List<CategoryViewModel> Categories { get; set; }
        [BindProperty(SupportsGet =true)]
        public List<string> Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal Amount { get; set; } = 0;
        public IndexModel( CategoriesService categoriesService)
        {        
            _categoriesService = categoriesService;
        }        
        public async Task OnGetAsync()
        {
            Categories = await _categoriesService.GetCategories();            
        }
    }
}
