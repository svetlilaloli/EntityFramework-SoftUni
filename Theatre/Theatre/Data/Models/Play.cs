using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Theatre.Data.Models.Enums;

namespace Theatre.Data.Models
{
    public class Play
    {
        public Play()
        {
            Casts = new HashSet<Cast>();
            Tickets = new HashSet<Ticket>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_TITLE_LENGTH), Required]
        public string Title { get; set; }
        [DataType(DataType.Time), DisplayFormat(DataFormatString = Constants.TIME_FORMAT), Range(typeof(TimeSpan), Constants.MIN_DURATION, Constants.MAX_DURATION), Required]
        public TimeSpan Duration { get; set; }
        [Range(Constants.MIN_RATING, Constants.MAX_RATING), Required]
        public float Rating { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [MaxLength(Constants.MAX_DESCRIPTION_LENGTH), Required]
        public string Description { get; set; }
        [MinLength(Constants.MIN_STRING_LENGTH), MaxLength(Constants.MAX_STRING_LENGTH), Required]
        public string Screenwriter { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
