using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.Data.Models
{
    public class Task
    {
        public Task()
        {
            EmployeesTasks = new HashSet<EmployeeTask>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.MIN_NAME_LENGTH), MaxLength(Constants.MAX_NAME_LENGTH), Required]
        public string Name { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public ExecutionType ExecutionType { get; set; }
        [Required]
        public LabelType LabelType { get; set; }
        [ForeignKey(nameof(Project)), Required]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}