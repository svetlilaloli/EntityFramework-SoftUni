using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            EmployeesTasks = new HashSet<EmployeeTask>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.MIN_USERNAME_LENGTH), MaxLength(Constants.MAX_NAME_LENGTH), RegularExpression(Constants.USERNAME_REGEX), Required]
        public string Username { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [RegularExpression(Constants.PHONE_REGEX), Required]
        public string Phone { get; set; }
        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
