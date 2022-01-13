using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fictional_Customers.Models
{
    public class Assignments
    {
        public enum StackType
        {
            MEAN,
            MERN,
            MEVN,
            LAMP,
            Serverless
        }
        public int AssignmentsId { get; set; }
        [Required]
        public string Company { get; set; }
        [DisplayName("Stack Type")]
        public StackType Stack { get; set; }
        public string Task { get; set; }
        [DisplayName("Programming Language")]
        public string ProgLang { get; set; }
        [DisplayName("Date")]
        [Required]
        public DateTime StartDate { get; set; }
        public List<Staff> Employee { get; set; } = new List<Staff>();

        public string GetAllEmployees()
        {
            string employees = string.Empty;
            for (int i = 0; i < Employee.Count; i++)
            {
                if (i < Employee.Count - 1)
                {
                    employees += $"{Employee[i].Name}, ";
                }
                else
                {
                    employees += $"{Employee[i].Name}";
                }
            }
            //Employee.ForEach(e => employees += $"{e.Name}, ");
            return employees;
        }

    }
}
