using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webodev.Data;
using webodev.Models;

namespace webodev.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalonController : Controller
    {
        private readonly UygulamaDbContext _context;

        public SalonController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Salon/GetCalisanlarBySalon/{salonId}")]
        public IActionResult GetCalisanlarBySalon(int salonId)
        {
            var calisanlar = _context.Calisanlar
                .Where(c => c.SalonId == salonId)
                .Select(c => new { c.Id, c.Ad })
                .ToList();
            return Json(calisanlar);
        }

        // Salonları Listeleme
        public IActionResult Index()
        {
            var salonlar = _context.Salonlar.Include(s => s.Calisanlar).ToList();
            return View(salonlar);
        }

        // Yeni Salon Ekleme Formu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni Salon Ekleme POST İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salonlar.Add(salon);
                _context.SaveChanges();
                TempData["Success"] = "Salon başarıyla eklendi!";
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // Salon Düzenleme Formu
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // Salon Düzenleme POST İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salonlar.Update(salon);
                _context.SaveChanges();
                TempData["Success"] = "Salon başarıyla güncellendi!";
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // Salon Silme Formu
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var salon = _context.Salonlar
                .Include(s => s.Calisanlar)
                .FirstOrDefault(s => s.Id == id);

            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // Salon Silme POST İşlemi
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon == null)
            {
                return NotFound();
            }

            _context.Salonlar.Remove(salon);
            _context.SaveChanges();
            TempData["Success"] = "Salon başarıyla silindi!";
            return RedirectToAction(nameof(Index));
        }
    }
}
