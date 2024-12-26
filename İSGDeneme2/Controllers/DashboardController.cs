using İSGDeneme2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace İSGDeneme2.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Employees tablosundan benzersiz (distinct) departmanları çek
            var departments = _context.Employees
                .Where(e => !string.IsNullOrEmpty(e.Department)) // Boş olmayan departmanları filtrele
                .Select(e => e.Department) // Sadece Department kolonunu al
                .Distinct() // Benzersiz hale getir
                .ToList(); // Listeye dönüştür

            // ViewBag'e ata
            ViewBag.Departments = departments;

            var viewModel = new DashboardViewModel
            {
                RecentEmployees = _context.Employees
            .OrderByDescending(e => e.Employeesdate)
            .Take(5)
            .ToList(),

                RecentReports = _context.Reports
            .OrderByDescending(e => e.CreatedAt)
            .Take(5)
            .ToList(),

                PersonelSayısı = _context.Employees.Count(),
                RaporSayısı = _context.Reports.Count()
            };


            #region Genel Tablo Bilgileri

            var personelSayısı = _context.Employees?.Count() ?? 0;
            var raporSayısı = _context.Reports?.Count() ?? 0;

            ViewBag.Personel = personelSayısı;
            ViewBag.RaporSayısı = raporSayısı;
            #endregion

            return View(viewModel);

        }

        public IActionResult Filter(string department, DateTime? startDate, DateTime? endDate)
        {
            // Tüm çalışanları al
            var query = _context.Employees.AsQueryable();

            // Departman filtresi uygula
            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(e => e.Department == department);
            }

            // Başlangıç tarihi filtresi uygula
            if (startDate.HasValue)
            {
                query = query.Where(e => e.Employeesdate >= startDate.Value);
            }

            // Bitiş tarihi filtresi uygula
            if (endDate.HasValue)
            {
                query = query.Where(e => e.Employeesdate <= endDate.Value);
            }

            // Sonuçları listele ve View'e gönder
            var filteredEmployees = query.ToList();
            return View("Index", filteredEmployees);
        }




    }
}
