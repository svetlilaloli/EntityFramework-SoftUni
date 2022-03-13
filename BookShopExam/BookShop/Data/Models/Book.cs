using BookShop.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Models
{
    public class Book
    {
        public Book()
        {
            AuthorsBooks = new HashSet<AuthorBook>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MinLength(Constants.NameMinLength), MaxLength(Constants.NameMaxLength)]
        public string Name { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Range((double)Constants.MinPrice, (double)Constants.MaxPrice)]
        public decimal? Price { get; set; }
        [Range(Constants.MinPages, Constants.MaxPages)]
        public int? Pages { get; set; }
        [Required]
        public DateTime PublishedOn { get; set; }
        public virtual ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}
