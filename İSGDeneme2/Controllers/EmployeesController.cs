using İSGDeneme2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace İSGDeneme2.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchTerm, string department)
        {
            // Benzersiz departmanları ViewBag'e ekle
            var departments = _context.Employees
                .Where(e => !string.IsNullOrEmpty(e.Department))
                .Select(e => e.Department)
                .Distinct()
                .ToList();

            ViewBag.Departments = departments;

            // Çalışanları departmana göre filtrele
            var employees = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(department))
            {
                // Seçilen departmana göre filtreleme yap
                employees = employees.Where(e => e.Department == department);
            }

            // Arama terimi varsa, adı, pozisyonu veya departmanı arar
            if (!string.IsNullOrEmpty(searchTerm))
            {
                employees = employees.Where(e => e.FullName.Contains(searchTerm) ||
                                                  e.Positions.Contains(searchTerm) ||
                                                  e.Department.Contains(searchTerm));
            }

            // View'e gönderilecek modeli oluştur
            var viewModel = new EmployeeViewModel
            {
                Employees = employees.ToList()
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult CreateEmployees(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Veritabanına ekleme işlemi
                var employee = new Employee
                {
                    FullName = model.FullName,
                    Positions = model.Positions,
                    Department = model.Department,
                    Employeesdate = DateTime.Now // Örnek bir tarih
                };

                _context.Employees.Add(employee); // Veritabanına ekle
                _context.SaveChanges();          // Değişiklikleri kaydet

                return RedirectToAction("Index"); // Başarılıysa listeleme sayfasına yönlendir
            }

            // Hata varsa aynı sayfayı döndür
            return View("Index");
        }

        public IActionResult Personel(string department, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(e => e.Department == department);
            }

            if (startDate.HasValue)
            {
                query = query.Where(e => e.Employeesdate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(e => e.Employeesdate <= endDate.Value);
            }

            var departments = _context.Employees
                .Where(e => !string.IsNullOrEmpty(e.Department))
                .Select(e => e.Department)
                .Distinct()
                .ToList();


            return View();
        }



    }
}
