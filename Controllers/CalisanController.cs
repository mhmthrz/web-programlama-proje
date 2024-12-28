using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webodev.Models;
using webodev.Data;
using Microsoft.AspNetCore.Authorization;

namespace webodev.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CalisanController : Controller
    {
        private readonly UygulamaDbContext _context;

        public CalisanController(UygulamaDbContext context)
        {
            _context = context;
        }

        // Çalışanları Listeleme
        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar
                .Include(c => c.Islem)
                .Include(c => c.Salon)
                .ToList();

            return View(calisanlar);
        }

        // Yeni Çalışan Ekleme Formu
        public IActionResult Create()
        {
            ViewBag.Salonlar = _context.Salonlar.ToList(); // Salonları doldur
            ViewBag.Islemler = _context.Islemler.ToList(); // İşlemleri doldur
            return View();
        }

        // Yeni Çalışan Ekleme POST İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Calisanlar.Add(calisan);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Çalışan başarıyla eklendi!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Bir hata oluştu: {ex.Message}";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Lütfen tüm alanları doldurunuz.";
            }

            // Hatalı durumda tekrar ViewBag'leri doldur
            ViewBag.Salonlar = _context.Salonlar.ToList();
            ViewBag.Islemler = _context.Islemler.ToList();
            return View(calisan);
        }
         // Çalışanı Silme Formu (GET)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var calisan = _context.Calisanlar
                .FirstOrDefault(c => c.Id == id);

            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // Çalışanı Silme İşlemi (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var calisan = _context.Calisanlar.Find(id);

            if (calisan == null)
            {
                return NotFound();
            }

            try
            {
                _context.Calisanlar.Remove(calisan);
                _context.SaveChanges();
                TempData["Success"] = "Çalışan başarıyla silindi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
