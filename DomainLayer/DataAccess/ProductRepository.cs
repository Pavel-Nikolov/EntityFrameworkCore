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

        protected override IEnumerable<Product> LoadCollection(DbSet<Product> dbSet)
        {
            return dbSet.Include(x => x.Brand).Include(x => x.Users);
        }

        protected override void LoadEntity(Product entity)
        {
            context.Entry<Product>(entity).Reference(x => x.Brand).Load();
            context.Entry<Product>(entity).Collection(x => x.Users).Load();
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
