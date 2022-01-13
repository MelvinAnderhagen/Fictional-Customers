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
        public List<Assignments> Assignments { get; set; } = new List<Assignments>();

        public string GetAllAssignments()
        {
            string assignment = string.Empty;
            for (int i = 0; i < Assignments.Count; i++)
            {
                if (i < Assignments.Count - 1)
                {
                    assignment += $"{Assignments[i].Company}, ";
                }
                else
                {
                    assignment += $"{Assignments[i].Company}";
                }
            }
            //Employee.ForEach(e => employees += $"{e.Name}, ");
            return assignment;
        }
    }
}
