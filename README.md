# Fikir Havuzu - Staj Projesi

Bu proje, **TRTek** bünyesinde gerçekleştirdiğim staj çalışması kapsamında geliştirilmiş bir **Fikir ve Değerlendirme Yönetim Sistemi** web uygulamasıdır. Kurum çalışanlarının süreç iyileştirici veya yenilikçi fikirlerini doküman desteğiyle paylaşabildiği, yetkili roller tarafından değerlendirilip puanlandığı çok katmanlı bir mimariye sahiptir.

## Kullanılan Teknolojiler

- **Framework & Dil:** .NET 8.0, C#, ASP.NET Core MVC
- **Mimari Yapı:** Çok Katmanlı Mimari (N-Tier Architecture), Repository & Service Pattern
- **Veritabanı & ORM:** Microsoft SQL Server, Entity Framework Core (Code-First)
- **Kütüphaneler & Paketler:** FluentValidation, AutoMapper, BCrypt.Net
- **Ön Yüz (Frontend):** Razor Views, HTMX, Bootstrap 5, FontAwesome

## Kurulum ve Çalıştırma

### Gereksinimler

- .NET 8.0 SDK
- Microsoft SQL Server veya LocalDB

### 1. Projeyi Klonlayın

```bash
git clone https://github.com/ilhanbatinaydin/FikirHavuzu.git
cd FikirHavuzu
```

### 2. Veritabanı Bağlantısını Yapılandırın

FikirHavuzu.Web/appsettings.json dosyasındaki bağlantı dizesini yerel SQL Server ayarlarınıza göre düzenleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FikirHavuzuDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

### 3. Projeyi Başlatın

```bash
cd FikirHavuzu.Web
dotnet run
```

## Default Admin Hesabı

- **E-Mail:** admin@fikirhavuzu.com
- **Şifre:** Admin123!
