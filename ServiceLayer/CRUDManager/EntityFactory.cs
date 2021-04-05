using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.CRUDManager
{
    public static class EntityFactory
    {
        public static Brand GenerateBrand(object id, object name, List<Product> products)
        {
            return new Brand()
            {
                ID = int.Parse(id.ToString()),
                Name = name.ToString(),
                Products = products
            };
        }

        public static Product GenerateProduct(object barcode, object name, object quantity, object price, Brand brand, List<User> users)
        {
            return new Product()
            {
                Barcode = barcode.ToString(),
                Name = name.ToString(),
                Quantity = int.Parse(quantity.ToString()),
                Price = decimal.Parse(price.ToString()),
                Brand = brand,
                Users = users
            };
        }

        public static User GenerateUser(object id, object name, object age, List<Product> products)
        {
            return new User()
            {
                ID = int.Parse(id.ToString()),
                Name = name.ToString(),
                Age = ParseNullable(age.ToString(), "?"),
                Products = products
            };
        }

        /// <summary>
        /// Converts string to int?
        /// </summary>
        /// <param name="input">The string to be converted</param>
        /// <param name="nullSrting">The representation of the null value</param>
        /// <returns>A int? of value of the corresponding number or null if it is the nullString</returns>
        /// <exception cref="FormatException">The number is not a number or the null string</exception>

        public static int? ParseNullable(string input, string nullSrting)
        {
            if (input == nullSrting)
            {
                return null;
            }
            return int.Parse(input);
        }
    }
}
