using System.ComponentModel.DataAnnotations;

namespace BookShop.DataProcessor.ImportDto
{
    public class AuthorInputDto
    {
        [Required, MinLength(Constants.NameMinLength), MaxLength(Constants.NameMaxLength)]
        public string FirstName { get; set; }
        [Required, MinLength(Constants.NameMinLength), MaxLength(Constants.NameMaxLength)]
        public string LastName { get; set; }
        [Required, RegularExpression(Constants.PhonePattern)]
        public string Phone { get; set; }
        [Required, RegularExpression(Constants.EmailPattern)]
        public string Email { get; set; }
        public BookIdInputDto[] Books { get; set; }
    }
}
