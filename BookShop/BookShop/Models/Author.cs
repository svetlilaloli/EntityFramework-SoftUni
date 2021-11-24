using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        [Key]
        public int AutorId { get; set; }
        [MaxLength(50)]
        public string FirstName  { get; set; }
        [MaxLength (50)]
        public string LastName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
