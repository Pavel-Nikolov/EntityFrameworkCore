using DomainLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.ViewModels
{
    public class ProductVM
    {
        public string Barcode { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BrandVM Brand { get; set; }
        public List<UserVM> Users { get; set; }

        public ProductVM(Product product, bool loadConnection = false)
        {
            Barcode = product.Barcode;
            Name = product.Name;
            Quantity = product.Quantity;
            Price = product.Price;

            if (loadConnection)
            {
                product.Brand = product.Brand;
                Brand = new BrandVM(product.Brand);

                Users = new List<UserVM>();
                foreach (var item in product.Users)
                {
                    Users.Add(new UserVM(item));
                }
            }
        }
    }
}