using webodev.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace webodev.Data
{
    public class UygulamaDbContext : IdentityDbContext<Kullanici>
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Randevu - Çalışan İlişkisi
    modelBuilder.Entity<Randevu>()
        .HasOne(r => r.Calisan)
        .WithMany(c => c.Randevular)
        .HasForeignKey(r => r.CalisanId)
        .OnDelete(DeleteBehavior.Restrict);

    // Randevu - İşlem İlişkisi
    modelBuilder.Entity<Randevu>()
        .HasOne(r => r.Islem)
        .WithMany()
        .HasForeignKey(r => r.IslemId)
        .OnDelete(DeleteBehavior.Restrict);
}



    }
}
