using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webodev.Models;

namespace webodev.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Register View
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register POST
     [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Register(RegisterModel model)
{
    if (ModelState.IsValid)
    {
        // E-posta kontrolü
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError(string.Empty, "Bu e-posta adresi zaten kullanılıyor.");
            return View(model);
        }

        // Şifre politikası kontrolü
        var errors = new List<string>();

        if (model.Password.Length < 6)
            errors.Add("Şifre en az 6 karakter uzunluğunda olmalıdır.");

        if (!model.Password.Any(char.IsUpper))
            errors.Add("Şifre en az bir büyük harf içermelidir.");

        if (!model.Password.Any(char.IsLower))
            errors.Add("Şifre en az bir küçük harf içermelidir.");

        if (!model.Password.Any(char.IsDigit))
            errors.Add("Şifre en az bir rakam içermelidir.");

        if (!model.Password.Any(ch => !char.IsLetterOrDigit(ch)))
            errors.Add("Şifre en az bir özel karakter içermelidir (örneğin: @, #, $, vb.).");

        if (errors.Any())
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return View(model);
        }

        var user = new Kullanici
        {
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.TelefonNumarasi, // Telefon numarası burada kaydediliyor
            Yas = model.Yas
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Kullanıcı"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Kullanıcı"));
            }

            await _userManager.AddToRoleAsync(user, "Kullanıcı");

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    return View(model);
}




        // Login View
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login POST
        // Login POST
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginModel model)
{
    if (ModelState.IsValid)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (passwordCheck.Succeeded)
            {
                await _signInManager.SignInAsync(user, model.RememberMe);

                // Kullanıcının rolünü kontrol et
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", "Admin"); // Admin rolü varsa Admin Paneline yönlendir
                }

                return RedirectToAction("Index", "Home"); // Normal kullanıcı için ana sayfaya yönlendir
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Şifre yanlıştır.");
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
        }
    }

    return View(model);
}



        // Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
