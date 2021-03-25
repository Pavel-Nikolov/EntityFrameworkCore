using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.DataAccess
{
    public class BrandRepository : Repository<Brand, int>
    {        
        public BrandRepository(Context context) : base(context, context.Brand)
        {

        }

        protected override IEnumerable<Brand> LoadCollection(DbSet<Brand> dbSet)
        {
            return dbSet.Include(x => x.Products);
        }

        protected override void LoadEntity(Brand entity)
        {
            context.Entry<Brand>(entity).Collection(e => e.Products).Load();
        }

        protected override void MapConnections(Brand brand)
        {
            if (brand.Products == null)
            {
                return;
            }
            List<Product> products = new List<Product>();
            Product productFromDb;
            for (int i = 0; i < products.Count; i++)
            {
                productFromDb = context.Product.Find(products[i].Barcode);
                if (productFromDb != null)
                {
                    products[i] = productFromDb;
                }
            }
            brand.Products = products;
        }

    }
}
