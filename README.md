# Enoca .NET Challenge - Kargo Yönetim API

Bu proje, Enoca değerlendirme süreci kapsamında geliştirilmiş bir kargo desi hesaplama ve sipariş yönetim API'sidir. Proje, N-Tier (Çok Katmanlı) mimari standartlarına uygun olarak tasarlanmış ve kodların sürdürülebilirliğine odaklanılmıştır.

## Proje Detayları ve İş Kuralları

* **Otomatik Kargo Atama Algoritması:** Sipariş oluşturulurken API sadece desi bilgisini alır. Sistem arka planda kargo konfigürasyonlarını tarar:
  * Desi değeri bir firmanın aralığına uyuyorsa, en uygun fiyatlı olanı otomatik seçer.
  * Desi değeri hiçbir firmanın aralığına uymuyorsa, desi sınırına en yakın firmayı bulur ve aşım yapılan her 1 desi için ekstra ücret hesaplayarak siparişi oluşturur.
* **Standart CRUD İşlemleri:** Kargo firmaları, konfigürasyonlar ve siparişler için tüm uç noktalar (GET, POST, PUT, DELETE) hazırlanmış ve API üzerinden dışarıya string response ("Sipariş eklendi." vb.) dönecek şekilde ayarlanmıştır.
* **Arka Plan Görevleri (Bonus):** Hangfire kurularak sisteme entegre edilmiştir. Kargo maliyetlerini gün bazında gruplayıp `CarrierReports` tablosuna yazan bir cron job mevcuttur.

## Kullanılan Teknolojiler

* .NET Core 6.0 Web API
* Entity Framework Core (Code-First Yaklaşımı)
* MS SQL Server
* Hangfire
* Swagger UI
* N-Tier Architecture (Core, Data, Service, API)

## Kurulum ve Çalıştırma Adımları

1. Repoyu bilgisayarınıza klonlayın.
2. Visual Studio'da projeyi açın ve `Package Manager Console` penceresini açın.
3. Default Project (Varsayılan Proje) olarak **`EnocaChallenge.Data`** katmanını seçin ve `Update-Database` komutunu çalıştırarak veritabanı tablolarını oluşturun.
4. Sistemin çalışmasını test edebilmek için, projenin ana dizininde (`Root`) bulunan **`SQLQuery1.sql`** dosyasındaki script'i MSSQL üzerinde çalıştırarak örnek kargo firması konfigürasyonlarını içeri aktarın.
5. Projeyi çalıştırın (F5). Açılan Swagger arayüzü üzerinden uç noktaları test edebilirsiniz.
6. Arka plan görevlerini izlemek için tarayıcınızdan `https://localhost:[PORT]/hangfire` adresine gidebilirsiniz.

---
**Geliştirici:** Mehmet Emre Tek
