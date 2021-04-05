using DomainLayer.DataAccess;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.CRUDManager
{
    internal class DBFinder : IDisposable
    {
        private Context context;

        public DBFinder(Context context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            ((IDisposable)context).Dispose();
        }

        public List<Product> GetProducts(string[] keys)
        {
            List<Product> products = new List<Product>();
            Product product;
            foreach (var item in keys)
            {
                product = context.Product.Find(item);
                if (product == null)
                {
                    throw new InvalidOperationException($"There is no product with barcode {item}");
                }
                products.Add(product);
            }
            return products;
        }

        public List<User> GetUsers(int[] keys)
        {
            User user;
            List<User> users = new List<User>();
            foreach (var item in keys)
            {
                user = context.Users.Find(item);
                if (user == null)
                {
                    throw new InvalidOperationException($"There is no user with ID {item}");
                }
                users.Add(user);
            }
            return users;
        }

        public Brand GetBrand(int id)
        {
            Brand brand = context.Brand.Find(id);
            if (brand == null)
            {
                throw new InvalidOperationException($"There is no brand with ID {id}");
            }
            return brand;
        }
    }
}
