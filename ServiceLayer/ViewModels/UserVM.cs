using DomainLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.ViewModels
{
    public class UserVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public List<ProductVM> Products { get; set; }

        public UserVM(User user, bool loadConnection = false)
        {
            ID = user.ID;
            Name = user.Name;
            Age = user.Age;
            if (loadConnection)
            {
                Products = new List<ProductVM>();
                foreach (var item in user.Products)
                {
                    Products.Add(new ProductVM(item));
                }
            }
        }
    }
}