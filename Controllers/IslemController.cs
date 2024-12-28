using Microsoft.AspNetCore.Mvc;
using webodev.Models;
using webodev.Data;
using Microsoft.AspNetCore.Authorization;

namespace webodev.Controllers
{
    [Authorize(Roles = "Admin")]

    public class IslemController : Controller
    {
        private readonly UygulamaDbContext _context;

        public IslemController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult Index() {

            return View();

        }

        

        // Tüm İşlemleri Listeleme
        [HttpGet]
        [Route("api/islem")]
        
        public IActionResult GetAll()
        {
            var islemler = _context.Islemler.ToList();
            return Ok(islemler);
        }

        // Belirli bir İşlemi Getirme
        [HttpGet]
        [Route("api/islem/{id}")]
        public IActionResult GetById(int id)
        {
            var islem = _context.Islemler.Find(id);
            if (islem == null)
            {
                return NotFound(new { message = "İşlem bulunamadı." });
            }
            return Ok(islem);
        }

        // Yeni İşlem Ekleme
         [HttpPost]
        [Route("api/islem")]
        public IActionResult Create([FromBody] Islem islem)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { message = "Geçersiz veri gönderildi.", errors });
            }

            try
            {
                _context.Islemler.Add(islem);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = islem.IslemId }, islem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Veritabanı hatası: {ex.Message}" });
            }
        }

        // İşlem Güncelleme
    [HttpPut]
[Route("api/islem/{id}")]
public IActionResult Update(int id, [FromBody] Islem islem)
{
    // ID eşleşmesini kontrol et
    if (id != islem.IslemId)
    {
        // Eğer ID eşleşmiyorsa, JSON'daki islemId'yi URL'deki ID ile güncelleyebilirsiniz.
        islem.IslemId = id; 
    }

    // Veritabanında ilgili işlemi bul
    var existingIslem = _context.Islemler.Find(id);
    if (existingIslem == null)
    {
        return NotFound(new { message = "İşlem bulunamadı." });
    }

    try
    {
        // İşlem güncelleme
        existingIslem.Ad = islem.Ad;
        existingIslem.Ucret = islem.Ucret;

        _context.SaveChanges();

        return Ok(new { message = "İşlem başarıyla güncellendi.", islem = existingIslem });
    }
    catch (Exception ex)
    {
        return BadRequest(new { message = $"Veritabanı hatası: {ex.Message}" });
    }
}





        // İşlem Silme
         [HttpDelete]
        [Route("api/islem/{id}")]
        public IActionResult Delete(int id)
        {
            var islem = _context.Islemler.Find(id);
            if (islem == null)
            {
                return NotFound(new { message = "İşlem bulunamadı." });
            }

            try
            {
                _context.Islemler.Remove(islem);
                _context.SaveChanges();
                return Ok(new { message = "İşlem başarıyla silindi!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Veritabanı hatası: {ex.Message}" });
            }
        }
    }
}
