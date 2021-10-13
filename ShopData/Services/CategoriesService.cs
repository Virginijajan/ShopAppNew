using Microsoft.EntityFrameworkCore;
using ShopService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.ViewModels
{
    public class CategoriesService
    {
        public string Message = "";
        private readonly AppDbContext _db;      
        public CategoriesService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await _db.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ItemViewModels = c.Items.Select(i=>new ItemViewModel 
                {
                    Id=i.Id,
                    CategoryId=(int)i.CategoryId,
                    Description=i.Description,
                    Quantity=i.Quantity,
                    Price=i.Price,
                    Image=i.Image                   

                }).ToList()
            }).ToListAsync();           
        }
        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            return await _db.Categories.Where(c => c.Id == id).Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).SingleOrDefaultAsync();
        }
        public async Task UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var category =await _db.Categories.FindAsync(categoryViewModel.Id);
            category.Name = categoryViewModel.Name;
            Message = $"Category {category.Name} is updated.";
            await _db.SaveChangesAsync();
        }
        public async Task AddNewCategory(CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                Name = categoryViewModel.Name,
                Items = new List<Item>()
            };
            _db.Add(category);
            Message = $"Category {category.Name} is created.";
            await _db.SaveChangesAsync();
        }
        public async Task DeleteCategory(int id)
        {           
            var category =await _db.Categories.Where(c => c.Id == id).Include(c=>c.Items).Select(c => c).FirstOrDefaultAsync();
            if (category!=null&&category.Items.Count == 0)
            {
                _db.Remove(category);  
                Message = $"Category {category.Name} is deleted.";
                await _db.SaveChangesAsync();
            }
            else
                Message = $"Category {category.Name} can not be deleted. It has items.";
        }
    }
}
