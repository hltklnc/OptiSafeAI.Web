using System.ComponentModel.DataAnnotations;

namespace İSGDeneme2.Models
{
    public class EmployeeViewModel
    {
        [Required] // Zorunlu alanlar için
        public string FullName { get; set; }

        [Required]
        public string Positions { get; set; }

        [Required]
        public string Department { get; set; }

        // Eğer tabloyu listelemek için kullanıyorsanız:
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }



}
