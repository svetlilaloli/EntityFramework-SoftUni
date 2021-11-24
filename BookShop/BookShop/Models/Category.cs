﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Category
    {
        public Category()
        {
            CategoryBooks = new HashSet<BookCategory>();
        }
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<BookCategory> CategoryBooks { get; set; }
    }
}
