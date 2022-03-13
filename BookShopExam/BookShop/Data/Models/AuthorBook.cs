using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Data.Models
{
    public class AuthorBook
    {
        [ForeignKey(nameof(Author)), Required]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        [ForeignKey(nameof(Book)), Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}