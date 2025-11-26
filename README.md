📚 Social Library – Backend API

Social Library, kullanıcıların film ve kitap içeriklerini keşfedebildiği, takip edebildiği, oylayabildiği ve sosyal etkileşim kurabildiği bir platformun backend API projesidir.
Bu proje .NET 8, Clean Architecture, Entity Framework Core, MySQL, ve JWT Authentication yapıları kullanılarak geliştirilmiştir.

🚀 Özellikler
🔐 Kullanıcı Yönetimi

Kullanıcı kayıt & giriş (Register / Login)

JWT Access Token

Refresh Token yenileme

Şifre sıfırlama (Password Reset Token)

Profil bilgisi güncelleme

📚 İçerik Yönetimi

Kitap / Film içerikleri ekleme

Tür (Genre) yönetimi

İçerik detay, filtreleme ve arama

İçeriğe göre listeleme

⭐ Sosyal Etkileşim

Kullanıcılar arası takip sistemi (Follow)

İçerik değerlendirme (Review)

Beğeni (Review Like)

Puanlama (Rating)

Aktivite akışı (Activity Feed)

📌 Kütüphane Yönetimi

Kullanıcıların okudukları / izledikleri içerik takibi

Kütüphane durumları:
Planned, Watching/Reading, Completed, Dropped, On-Hold

🏗 Mimari

Domain Katmanı → Entity, Enum, Rules

Application Katmanı → Business Logic

Infrastructure Katmanı → EF Core + MySQL

API Katmanı → Controller & Endpoints

🛠 Kullanılan Teknolojiler
Teknoloji	Açıklama
.NET 8	Backend API
Entity Framework Core	ORM
MySQL + Pomelo	Veritabanı
Clean Architecture	Katmanlı yapı
JWT Authentication	Kimlik doğrulama
AutoMapper	DTO – Entity dönüşümleri
Swagger	API dokümantasyonu
📂 Proje Klasör Yapısı
/Domain
   ├── Entities
   ├── Enums
   └── ValueObjects

/Application
   ├── Interfaces
   ├── Services
   └── DTOs

/Infrastructure
   ├── Persistence
   ├── Migrations
   └── Repositories

/Api
   ├── Controllers
   ├── Program.cs
   ├── appsettings.json
   └── Middlewares

▶️ Projeyi Çalıştırma
1️⃣ Bağımlılıkları yükle
dotnet restore

2️⃣ Veritabanını migrate et
dotnet ef database update

3️⃣ API’yi başlat
dotnet run --project Api


API şu adreste çalışır:

https://localhost:5001
http://localhost:5000


Swagger:

https://localhost:5001/swagger

🔐 Ortam Değerleri (appsettings.json)
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=social_library;user=root;password=yourpassword"
},
"Jwt": {
  "Key": "your_secret_key",
  "Issuer": "SocialLibraryAPI",
  "Audience": "SocialLibraryAPIUsers",
  "ExpireMinutes": 15
}

🤝 Katkıda Bulunma

Her türlü katkı, öneri ve pull request’e açığız!

Repo'yu forkla

Yeni branch oluştur

Geliştirmeyi yap

Pull request gönder
