using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    [Index(nameof(Name))]
    public class User : IEntity<int>
    {
        [Key]
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter name")]
        public string Name { get; set; }
        [CustomValidation(typeof(Validator), nameof(Validator.ValidateNullable), ErrorMessage = "Age cannot be negative")]
        public int? Age { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        [NotMapped]
        public int Key => ID;
        
        [NotMapped]
        public string Index => Name;

        public void UpdateEntity(IEntity<int> newEntity)
        {
            if (newEntity is User user)
            {
                ID = user.ID;
                Name = user.Name;
                Age = user.Age;
                Products = new List<Product>(user.Products);
            }
        }

        public User()
        {
            Products = new List<Product>();
        }

        
    }
}