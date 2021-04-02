using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.DataAccess
{
    public class ProductRepository : Repository<Product, string>
    {
        public ProductRepository(Context context):base(context, context.Product)
        {

        }

       
        protected override void MapConnections(Product entity)
        {
            Brand brandFromDb = context.Brand.Find(entity.Brand.ID);
            if (brandFromDb != null)
            {
                entity.Brand = brandFromDb;
            }

            if (entity.Users == null)
            {
                return;
            }

            List<User> users = new List<User>(entity.Users);
            User userFromDb;
            for (int i = 0; i < users.Count; i++)
            {
                userFromDb = context.Users.Find(users[i].ID);
                if (userFromDb != null)
                {
                    users[i] = userFromDb;
                }
            }
            entity.Users = users;
        }
    }
}
