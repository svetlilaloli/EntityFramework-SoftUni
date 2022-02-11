using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class User
    {
        public User()
        {
            Cards = new HashSet<Card>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.USERNAME_MIN_LENGTH), MaxLength(Constants.USERNAME_MAX_LENGTH), Required]
        public string Username { get; set; }
        [RegularExpression(Constants.FULL_NAME_PATTERN), Required]
        public string FullName { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [Range(Constants.MIN_AGE, Constants.MAX_AGE), Required]
        public int Age { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
