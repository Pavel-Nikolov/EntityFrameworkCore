using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Models
{
    [Index(nameof(Name))]
    public class Brand : IEntity<int>
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter name", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        public virtual ICollection<Product> Products { get; set; }


        [NotMapped]
        public int Key => ID;
        [NotMapped]
        public bool LoadedConections { get; set; }
        [NotMapped]
        public string Index => Name;

        public void UpdateEntity(IEntity<int> newEntity)
        {
            if (newEntity is Brand movie)
            {
                this.ID = movie.ID;
                this.Name = movie.Name;                
                this.Products = new List<Product>(movie.Products);
            }
            else
            {
                throw new InvalidOperationException("The updated and the updating entities must be from the same type");
            }
        }

        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
