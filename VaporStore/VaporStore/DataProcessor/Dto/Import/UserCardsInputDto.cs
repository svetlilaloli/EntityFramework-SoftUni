using System;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class UserCardsInputDto
    {
        [RegularExpression(Constants.FULL_NAME_PATTERN), Required]
        public string FullName { get; set; }
        [MinLength(Constants.USERNAME_MIN_LENGTH), MaxLength(Constants.USERNAME_MAX_LENGTH), Required]
        public string Username { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [Range(Constants.MIN_AGE, Constants.MAX_AGE), Required]
        public int Age { get; set; }
        public CardInputDto[] Cards { get; set; }
    }
}
