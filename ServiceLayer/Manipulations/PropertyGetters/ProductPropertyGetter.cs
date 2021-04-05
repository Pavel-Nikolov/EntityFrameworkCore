using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Manipulations.PropertyGetters
{
    public class ProductPropertyGetter : PropertyGetter<Product, string>
    {
        public ProductPropertyGetter(string propertyName) : base(propertyName)
        {
        }

        public override IComparable GetProperty(Product entity)
        {
            switch (propertyName.ToLower())
            {
                case "barcode": return entity.Barcode;
                case "name": return entity.Name;
                case "quantity": return entity.Quantity;
                case "price": return entity.Price;
                case "brand": return entity.Brand.ID;
                case "users": return entity.Users.Count;
                default:
                    throw new ArgumentException($"No such property as {propertyName}");
            }
        }
    }
}
