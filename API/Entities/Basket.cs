using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; } 
        public List<BasketItem> Items {get; set;} = new(); // == new List<BasketItem>();
        public void AddItem(Product product, int quantity)
        {
            if (Items.All(item => item.ProductId != product.Id)) 
            // check if product is not in list of items,
            // if not in list of items, add new item to list
            {
                Items.Add(new BasketItem{Product = product, Quantity = quantity});
            }

            // if the item already exists, update quantity
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem != null) existingItem.Quantity += quantity;
        }

        public void RemoveItem(int productId, int quantity) 
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Quantity -= quantity;
            if (item.Quantity == 0) Items.Remove(item);
        }
    }
}