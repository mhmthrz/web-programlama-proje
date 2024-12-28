using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webodev.Data;
using webodev.Models;

namespace webodev.Controllers
{
    public class RandevuAlController : Controller
{
    private readonly UygulamaDbContext _context;

    public RandevuAlController(UygulamaDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var randevular = _context.Randevular
            .Include(r => r.Calisan)
            .Include(r => r.Islem)
            .Include(r => r.Salon)
            .ToList();

        return View(randevular);
    }

    public IActionResult RandevuAl(int id)
{
    if (!User.Identity.IsAuthenticated)
    {
        return RedirectToAction("Login", "Account");
    }

    var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userId))
    {
        TempData["ErrorMessage"] = "Kullanıcı kimliği bulunamadı.";
        return RedirectToAction("Index");
    }

    var randevu = _context.Randevular.FirstOrDefault(r => r.Id == id);
    if (randevu == null || !randevu.uygun)
    {
        TempData["ErrorMessage"] = "Randevu mevcut değil veya uygun değil.";
        return RedirectToAction("Index");
    }

    // Randevunun uygunluğunu false yap ve kullanıcıya ata
    randevu.uygun = false;
    randevu.KullaniciId = userId;

    try
    {
        _context.SaveChanges();
        TempData["SuccessMessage"] = "Randevu başarıyla alındı!";
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"Bir hata oluştu: {ex.Message}";
    }

    return RedirectToAction("Index");
}

    public IActionResult Randevularim()
{
    if (!User.Identity.IsAuthenticated)
    {
        return RedirectToAction("Login", "Account");
    }

    var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userId))
    {
        TempData["ErrorMessage"] = "Kullanıcı kimliği bulunamadı.";
        return RedirectToAction("Index");
    }

    // Kullanıcıya ait randevuları getir
    var randevular = _context.Randevular
        .Where(r => r.KullaniciId == userId)
        .Include(r => r.Calisan)
        .Include(r => r.Islem)
        .Include(r => r.Salon)
        .ToList();

    if (!randevular.Any())
    {
        TempData["ErrorMessage"] = "Henüz bir randevunuz yok.";
    }

    return View(randevular);
}


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RandevuSil(int id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var randevu = _context.Randevular.FirstOrDefault(r => r.Id == id && r.KullaniciId == userId);

        if (randevu == null)
        {
            TempData["ErrorMessage"] = "Randevu bulunamadı veya silme yetkiniz yok.";
            return RedirectToAction("Randevularim");
        }

        try
        {
            // Randevuyu tekrar uygun hale getir
            randevu.uygun = true;
            randevu.KullaniciId = null;

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Randevu başarıyla silindi ve yeniden uygun hale getirildi!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bir hata oluştu: {ex.Message}";
        }

        return RedirectToAction("Randevularim");
    }
}

}
