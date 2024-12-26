using İSGDeneme2.Models;
using Microsoft.AspNetCore.Mvc;

namespace İSGDeneme2.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;
        public ReportsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var reports = _context.Reports.AsQueryable();

            var viewModel = new ReportViewModel
            {
                Reports = _context.Reports.ToList(),
                CurrentReport = new Report()
            };

            return View(viewModel);  
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]  
        public IActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                report.CreatedAt = DateTime.Now;
                _context.Reports.Add(report);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var report = _context.Reports.FirstOrDefault(r => r.Id == id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }
        [HttpPost]
        public IActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Reports.Update(report);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }
        public IActionResult Details (int id )
        {
            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }
        public IActionResult FilterReports(DateTime? startDate, DateTime? endDate)
        {
            var reports = _context.Reports.AsQueryable();

            // Eğer tarih verilmemişse, günümüzü al
            if (startDate.HasValue)
            {
                reports = reports.Where(r => r.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                reports = reports.Where(r => r.CreatedAt <= endDate.Value);
            }

            var viewModel = new ReportViewModel
            {
                Reports = reports.ToList(),
                StartDate = startDate,
                EndDate = endDate
            };

            return View("Index", viewModel);  // View'a doğru model gönderildiğinden emin olun
        }

    }
}
