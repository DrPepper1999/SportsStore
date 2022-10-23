using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart // Изминить добавить индексатор и разобраться с сериалезацией и дисиреалезацией
    {
        public List<CartLine> LineCollection { get; private set; } = new List<CartLine>();

        public Cart() { }
            
        [JsonConstructor]
        public Cart(List<CartLine> LineCollection) =>
            this.LineCollection = LineCollection;

        public void AddItem(Product product, int quantity)
        {
            var line = LineCollection.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if (line == null)
            {
                LineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            LineCollection.RemoveAll(l => l. Product.Id == product.Id);
        }
        public decimal ComputeTotalValue()
        {
            return LineCollection.Sum(e => e.Product.Price * e.Quantity);
        }
        public void Clear()
        {
            LineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
