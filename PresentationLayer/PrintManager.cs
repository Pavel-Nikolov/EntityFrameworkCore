using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PresentationLayer
{
    public static class PrintManager
    {
        public static void PrintBrand(BrandVM movieVM)
        {
            if (movieVM.Products == null)
            {
                Console.WriteLine($"|ID: {movieVM.ID,-5}|Name: {movieVM.Name,-20}|");
            }
            else
            {
                Console.WriteLine($"|ID: {movieVM.ID,-5}|Name: {movieVM.Name,-20}|Products: {string.Join(", ", movieVM.Products.Take(2).Select(x=>x.Name))+"...", -20}|");
            }
        }

        public static void PrintProduct(ProductVM productVM)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"|ID: {productVM.Barcode,-12}|Name: {productVM.Name,-20}|Quantity: {productVM.Quantity,-5}|Price: {productVM.Price,-7:C}|");
            if (productVM.Movie != null)
            {
                stringBuilder.Append($"Brand: {productVM.Movie.Name,-20}|");
            }
            if (productVM.Users !=null)
            {
                stringBuilder.Append($"Users: {string.Join(", ", productVM.Users.Take(2).Select(x => x.Name))+"...",-20}|");
            }
            Console.WriteLine(stringBuilder.ToString());
        }

        public static void PrintUsers(UserVM userVM)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"|ID: {userVM.ID,-5}|Name: {userVM.Name,-30}|Age: {PrintNullable(userVM.Age, "?"),-3}|");
            if (userVM.Products != null)
            {
                stringBuilder.Append($"Product: {string.Join(", ", userVM.Products.Take(2).Select(x => x.Name))+"...",-20}|");
            }
            Console.WriteLine(stringBuilder.ToString());
        }
        /// <summary>
        /// Converts nullable int into text representation
        /// </summary>
        /// <param name="value">The value to be converted</param>
        /// <param name="nullString">The string to be returned if the value is null</param>
        /// <returns> The string representation of the number or the nullString if it is null</returns>
        public static string PrintNullable(int? value, string nullString)
        {
            if (value.HasValue)
            {
                return value.Value.ToString();
            }
            return nullString;
        }
    }
}
