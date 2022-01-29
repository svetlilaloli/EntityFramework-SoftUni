using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Theatre.Data.Models
{
    public class Theatre
    {
        public Theatre()
        {
            Tickets = new HashSet<Ticket>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string Name { get; set; }
        [Range(Constants.MIN_NUMBER, Constants.MAX_NUMBER), Required]
        public sbyte NumberOfHalls { get; set; }
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string Director { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
