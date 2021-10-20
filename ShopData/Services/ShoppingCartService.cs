using ShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService.Services
{
    public class ShoppingCartService
    {
        public string Message = "";
        public Dictionary<int, int> ShoppingCart { get; set; } = new Dictionary<int, int>();
        
        public void AddItem(int id, int number)
        {           
            if (ShoppingCart.ContainsKey(id))
            {
                ShoppingCart[id] += number;
            }
            else
            {
                ShoppingCart.Add(id, number);
            }
            
            Message = "Item is added to shopping cart.";
        }
        public void DeleteItemFromShoppingCart(int id)
        {
            ShoppingCart.Remove(id);
        }       
    }
}
