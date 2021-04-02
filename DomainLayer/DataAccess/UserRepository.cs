using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DataAccess
{
    public class UserRepository : Repository<User, int>
    {
        public UserRepository(Context context):base(context, context.Users)
        {

        }

        

        protected override void MapConnections(User user)
        {
            if (user.Products == null)
            {
                return;
            }

            List<Product> products = new List<Product>(user.Products);
            Product productFromDb;

            for (int i = 0; i < products.Count; i++)
            {
                productFromDb = context.Product.Find(products[i].Barcode);
                if (productFromDb != null)
                {
                    products[i] = productFromDb;
                }
            }

            user.Products = products;
        }
    }
}
