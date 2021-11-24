using BookShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Book
    {
        public Book()
        {
            BookCategories = new HashSet<BookCategory>();
        }
        [Key]
        public int BookId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        public int Copies { get; set; }
        public decimal Price { get; set; }
        public EditionType  EditionType { get; set; }
        public AgeRestriction  AgeRestriction { get; set; }
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
