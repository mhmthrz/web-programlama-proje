using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webodev.Data;
using webodev.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UygulamaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddIdentity<Kullanici, IdentityRole>(options =>
{
    // Şifre gereksinimleri
    options.Password.RequireDigit = true; // En az bir rakam gerektir
    options.Password.RequiredLength = 6; // Minimum uzunluk 6
    options.Password.RequireLowercase = true; // Küçük harf gerektir
    options.Password.RequireUppercase = true; // Büyük harf gerektir
    options.Password.RequireNonAlphanumeric = true; // Özel karakter gerektir
})
.AddEntityFrameworkStores<UygulamaDbContext>()
.AddDefaultTokenProviders();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Kullanici>>();

    // Admin rolünü kontrol et, yoksa oluştur
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Belirli bir kullanıcıyı admin yapma
    var adminEmail = "B221210062@sakarya.edu.tr"; // Admin yapmak istediğiniz kullanıcı
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
