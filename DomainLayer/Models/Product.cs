using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    [Index(nameof(Name))]
    public class Product : IEntity<string>
    {
        [Key]
        public string Barcode { get; set; }
        [Required(ErrorMessage = "Enter name", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be positive")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Enter movie")]
        public virtual Brand Brand { get; set; }
        public virtual ICollection<User> Users { get; set; }


        [NotMapped]
        public string Key => Barcode;
        [NotMapped]
        public bool LoadedConections { get; set; }
        [NotMapped]
        public string Index => Name;

        public void UpdateEntity(IEntity<string> newEntity)
        {
            if (newEntity is Product order)
            {
                Barcode = order.Barcode;
                Name = order.Name;                
                Price = order.Price;
                Brand = order.Brand;
                Users = new List<User>(order.Users);
            }
            else
            {
                throw new InvalidOperationException("The updated and the updating entities must be from the same type");
            }
        }

        public Product()
        {
            Users = new List<User>();
        }
    }
}