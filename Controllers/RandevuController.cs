using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webodev.Models;
using webodev.Data;
using Microsoft.AspNetCore.Authorization;

namespace webodev.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RandevuController : Controller
    {
        private readonly UygulamaDbContext _context;

        public RandevuController(UygulamaDbContext context)
        {
            _context = context;
        }

        // Randevuları Listeleme
        public IActionResult Index()
        {
            var randevular = _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Include(r => r.Salon)
                .ToList();
            return View(randevular);
        }

        // Yeni Randevu Ekleme Formu
        public IActionResult Create()
        {
            ViewBag.Calisanlar = _context.Calisanlar.ToList();
            ViewBag.Salonlar = _context.Salonlar.ToList();
            return View();
        }

        // Yeni Randevu Ekleme POST İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     randevu.uygun = true;
                    _context.Randevular.Add(randevu);
                    _context.SaveChanges();
                    TempData["Success"] = "Randevu başarıyla oluşturuldu!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Veritabanı hatası: {ex.Message}";
                }
            }

            ViewBag.Calisanlar = _context.Calisanlar.ToList();
            ViewBag.Salonlar = _context.Salonlar.ToList();
            return View(randevu);
        }

        // Randevu Silme İşlemi
        public IActionResult Delete(int id)
        {
            var randevu = _context.Randevular.Find(id);

            if (randevu == null)
            {
                return NotFound();
            }

            _context.Randevular.Remove(randevu);
            _context.SaveChanges();
            TempData["Success"] = "Randevu başarıyla silindi!";
            return RedirectToAction(nameof(Index));
        }
    }
}
