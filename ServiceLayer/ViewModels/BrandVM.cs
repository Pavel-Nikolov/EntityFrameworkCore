using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ViewModels
{
    public class BrandVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public List<ProductVM> Products { get; set; }

        public BrandVM(Brand brand, bool loadConnection = false)
        {
            ID = brand.ID;
            Name = brand.Name;            
            if (loadConnection)
            {
                Products = new List<ProductVM>();
                foreach (var item in brand.Products)
                {
                    Products.Add(new ProductVM(item));
                }
            }
        }
    }
}
