using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Models
{
    public class Author
    {
        public Author()
        {
            AuthorsBooks = new HashSet<AuthorBook>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MinLength(Constants.NameMinLength), MaxLength(Constants.NameMaxLength)]
        public string FirstName { get; set; }
        [Required, MinLength(Constants.NameMinLength), MaxLength(Constants.NameMaxLength)]
        public string LastName { get; set; }
        [Required, RegularExpression(Constants.EmailPattern)]
        public string Email { get; set; }
        [Required, RegularExpression(Constants.PhonePattern)]
        public string Phone { get; set; }
        public virtual ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}
