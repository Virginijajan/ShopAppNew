using Microsoft.EntityFrameworkCore;
using ShopService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.ViewModels
{
    public class ShopItemsService
    {
        private readonly AppDbContext _db;
        private readonly CategoriesService _categoriesService;
        public string Message = "";
        public ShopItemsService(AppDbContext db, CategoriesService categoriesService)
        {
            _db = db;
            _categoriesService = categoriesService;
        }
        public async Task<CategoryViewModel> GetShopItemsByCategory(int id)
        {
            return await _db.Categories.Where(c => c.Id == id).Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ItemViewModels = c.Items.Select(i => new ItemViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Image = i.Image,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).SingleOrDefaultAsync();          
        }       
        public async Task<ItemViewModel> GetShopItemById(int itemId)
        {
            return await _db.Items.Where(i => i.Id == itemId).Select(i => new ItemViewModel
            {              
                Id = i.Id,
                CategoryId=(int)i.CategoryId,
                Name = i.Name,
                Description = i.Description,
                Image = i.Image,
                Quantity = i.Quantity,
                Price = i.Price
            }).FirstOrDefaultAsync();
        }       
        public async Task UpdateShopItemDb(ItemViewModel itemViewModel)
        {
            var item = await _db.Items.FindAsync(itemViewModel.Id);          
            item.Name = itemViewModel.Name;
            item.CategoryId = itemViewModel.CategoryId;
            item.Description = itemViewModel.Description;
            item.Image = itemViewModel.Image;
            item.Quantity = itemViewModel.Quantity;
            item.Price = itemViewModel.Price;
            Message = $"Shop item {item.Name} is updated.";
            await _db.SaveChangesAsync();
        }
        public async Task AddNewShopItemDb(ItemViewModel itemViewModel)
        {           
            var item = new Item
            {
            Name = itemViewModel.Name,
            CategoryId=itemViewModel.CategoryId,
            Description = itemViewModel.Description,
            Image = itemViewModel.Image,
            Quantity = itemViewModel.Quantity,
            Price = itemViewModel.Price,
            };          
            _db.Add(item);
            Message = $"Shop item {item.Name} is created.";
            await _db.SaveChangesAsync();
        }
        public async Task DeleteShopItem(int id)
        {
            var item = await _db.Items.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                Message = $"Shop item {item.Name} is deleted";
                _db.Remove(item);
                await _db.SaveChangesAsync();
            }
            else
                Message = $"Shop item is not found.";
        }
        public async Task UpdateShopItemQuantity(ItemViewModel itemViewModel)
        {
            var item = await _db.Items.FindAsync(itemViewModel.Id);
            item.Quantity = itemViewModel.Quantity;
            await _db.SaveChangesAsync();
        }
        public async Task<List<ItemViewModel>> GetShopItems()
        {
            return await _db.Items.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Image = i.Image,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToListAsync();
        }
        public async Task<List<ItemViewModel>> GetShoppingCartItems(Dictionary<int, int>items)
        {
            var itemsId = items.Keys;
            return await _db.Items.Where(i => itemsId.Contains(i.Id)).Select(i => new ItemViewModel
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Image = i.Image,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToListAsync();
        }
        public decimal GetShoppingCartAmount(Dictionary<int, int> items)
        {
            var itemsId = items.Keys;
            decimal amount = 0;
            var itemsAmount= _db.Items.Where(i => itemsId.Contains(i.Id)).Select(i => i.Price*items[i.Id]);
            foreach (var item in itemsAmount)
                amount += item;
          return amount;
        }
    }   
}
