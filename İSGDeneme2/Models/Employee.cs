using System.ComponentModel.DataAnnotations;

namespace İSGDeneme2.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Positions { get; set; }
        public string Department { get; set; }
        public DateTime Employeesdate { get; set; }
    }

}
