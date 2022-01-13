using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fictional_Customers.Models
{
    public class Staff
    {
        public long StaffId { get; set; }
        [DisplayName("Employee Name")]
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNmr { get; set; }
        public ICollection<Assignments> Assignments { get; set; }
    }
}
