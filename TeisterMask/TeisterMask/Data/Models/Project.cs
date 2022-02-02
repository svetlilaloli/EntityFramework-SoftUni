using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.MIN_NAME_LENGTH), MaxLength(Constants.MAX_NAME_LENGTH), Required]
        public string Name { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
