namespace İSGDeneme2.Models
{
    public class ReportViewModel
    {
        public IEnumerable<Report> Reports { get; set; }
        public Report CurrentReport { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
    }
}
