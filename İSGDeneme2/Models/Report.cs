using System.ComponentModel.DataAnnotations;

namespace İSGDeneme2.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string Content { get; set; } // Rapor içeriği

        public DateTime CreatedAt { get; set; }
    }
}
