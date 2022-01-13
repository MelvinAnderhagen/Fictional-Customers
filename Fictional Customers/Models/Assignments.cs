using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DisplayName("Programming Language")]
        public string ProgLang { get; set; }
        [DisplayName("Date")]
        [Required]
        public DateTime StartDate { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }

    }
}
