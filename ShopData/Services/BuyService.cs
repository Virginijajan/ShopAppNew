using ShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Services
{
    public class BuyService
    {
        private readonly ShopItemsService _shopItemsService;
        public string Message = "";
        public BuyService(ShopItemsService shopItemsService)
        {
            _shopItemsService = shopItemsService;
        }
        public async Task<decimal> BuyShopItem(int id, int number)
        {
            var item = await _shopItemsService.GetShopItemById(id);
            if (item.Quantity >= number && number>=0)
            {
                var amount = number * item.Price;
                item.Quantity = item.Quantity - number;
                await _shopItemsService.UpdateShopItemQuantity(item);
                Message = $"You have bought {number} kg {item.Name}. Total amount is {amount} EUR.";
                return amount;
            }
            else
            {
                Message = $"The number must be less or equal {item.Quantity}";
                return 0;
            } 
        }
    }
}
