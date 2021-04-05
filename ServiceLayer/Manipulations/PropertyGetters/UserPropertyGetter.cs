using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Manipulations.PropertyGetters
{
    public class UserPropertyGetter : PropertyGetter<User, int>
    {
        public UserPropertyGetter(string propertyName) : base(propertyName)
        {
        }

        public override IComparable GetProperty(User entity)
        {
            switch (propertyName.ToLower())
            {
                case "id": return entity.ID;
                case "name": return entity.Name;
                case "age": return entity.Age;
                case "product": return entity.Products.Count;
                default:
                    throw new ArgumentException($"No such property as {propertyName}");
            }
        }
    }
}
