﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductShop.Models
{
    public class User
    {
        public User()
        {
            ProductsSold = new List<Product>();
            ProductsBought = new List<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        [Required, MinLength(3)]
        public string LastName { get; set;}
        public int? Age { get; set; }
        [InverseProperty("Seller")]
        public ICollection<Product> ProductsSold { get; set; }
        [InverseProperty("Buyer")]
        public ICollection<Product> ProductsBought { get; set; }
    }
}
