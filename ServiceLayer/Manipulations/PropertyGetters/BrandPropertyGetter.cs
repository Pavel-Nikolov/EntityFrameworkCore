using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Manipulations.PropertyGetters
{
    public class BrandPropertyGetter : PropertyGetter<Brand, int>
    {
        public BrandPropertyGetter(string propertyName) : base(propertyName)
        {
        }

        public override IComparable GetProperty(Brand entity)
        {
            switch (base.propertyName.ToLower())
            {
                case "id": return entity.ID;
                case "name": return entity.Name;
                case "products": return entity.Products.Count;
                default:
                    throw new ArgumentException($"No such property as {propertyName}");
            }
        }
    }
}
