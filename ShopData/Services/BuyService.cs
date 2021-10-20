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
        private readonly ShoppingCartService _shoppingCartService;
        public List<string> Message =new List<string>();
        public decimal Amount = 0;
        public BuyService(ShopItemsService shopItemsService, ShoppingCartService shoppingCartService)
        {
            _shopItemsService = shopItemsService;
            _shoppingCartService = shoppingCartService;
        }
        public async Task BuyShopItem(int id, int number)
        {
            var item = await _shopItemsService.GetShopItemById(id);
            if (item.Quantity >= number && number>=0)
            {
                var amount = number * item.Price;
                item.Quantity = item.Quantity - number;
                await _shopItemsService.UpdateShopItemQuantity(item);
                Message.Add( $"You have bought {number} kg {item.Name}. Amount is {amount} EUR.");
                Amount+=amount;
            }
            else
            {
                Message.Add($"The number must be less or equal {item.Quantity}");
            } 
        }
        public async Task<bool> IsItemNumberValid(int id, int number)
        {
            var item = await _shopItemsService.GetShopItemById(id);
            int numberInTheShoppingCart = 0;
            if(_shoppingCartService.ShoppingCart.ContainsKey(id))
                numberInTheShoppingCart= _shoppingCartService.ShoppingCart[id];
            if (item.Quantity >= number+numberInTheShoppingCart && number > 0)
            {
                return true;
            }
            else 
            {
                Message.Add($"Total number of items in the shopping cart must be less or equal {item.Quantity}");
                return false;
            }           
        }
        public async Task BuyShopItems()
        {           
            foreach(var item in _shoppingCartService.ShoppingCart)
            {
                await BuyShopItem(item.Key, item.Value);
                _shoppingCartService.DeleteItemFromShoppingCart(item.Key);
            }
        }
    }
}
