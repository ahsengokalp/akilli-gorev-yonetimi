Akıllı Görev ve Öncelik Yönetim Sistemi

BIL440 – Yazılım Proje Yönetimi Final Projesi

1. Proje Tanımı

Bu proje, kullanıcıların görevlerini daha verimli şekilde planlamasını sağlamak amacıyla geliştirilmiş analitik odaklı bir görev yönetim sistemidir.
Sistem; görev ekleme, güncelleme, silme gibi temel işlemlerin yanında gecikme riski analizi, öncelik çakışması tespiti ve AI-destekli görev önerileri sunar.

Proje, Agile/Scrum yaklaşımı ile planlanmış ve backend servisleri üzerinden geliştirilmiştir.

2. Kullanılan Teknolojiler

.NET 8 – ASP.NET Core Web API

Entity Framework Core

SQLite

Swagger (OpenAPI)

Kural tabanlı AI-assisted analiz yaklaşımı

3. Sistem Mimarisi (Özet)

Backend tabanlı REST API

Katmanlar:

Models

Controllers

DTOs

Analytics

Kullanıcı etkileşimi Swagger üzerinden sağlanmıştır.

4. User Story – Endpoint Eşleştirmesi
   User Story Açıklama Endpoint
   US-1 Kullanıcıya özel görev alanı /api/Tasks?userId=
   US-2 Görev ekleme POST /api/Tasks
   US-3 Görev güncelleme PUT /api/Tasks/{id}
   US-4 Görev silme DELETE /api/Tasks/{id}
   US-5 Görev bağımlılığı tanımlama POST /api/TaskDependencies
   US-6 Gecikme riski analizi GET /api/Analytics/delay-risk
   US-7 Öncelik çakışması analizi GET /api/Analytics/priority-conflicts
   US-8 AI destekli görev önerisi GET /api/Analytics/recommendations
   US-9 Görev durumu takibi PUT /api/Tasks/{id}
5. Analitik ve AI-Assisted Yaklaşım
   5.1 Gecikme Riski Analizi

Görevler;

Öncelik seviyesi

Tahmini süre

Deadline yakınlığı

kriterlerine göre analiz edilerek riskScore değeri üretilir.

Bu analiz kural tabanlıdır ve otomatik olarak çalışır.

5.2 Öncelik Çakışması Analizi

Aynı zaman diliminde birden fazla High priority görev bulunması durumunda sistem çakışmayı tespit eder ve kullanıcıyı bilgilendirir.

Çıktı:

Çakışma tarihi

Etkilenen görevler

Görev sayısı

5.3 AI-Assisted Görev Önerileri

Sistem, görevler için aşağıdaki kriterlere göre öneriler üretir:

Yüksek öncelik + yakın deadline

Görev bağımlılıkları

Süre yoğunluğu

Bu yapı AI-assisted olup, karar destek mekanizması kural tabanlı olarak tasarlanmıştır.

6. Swagger ile Doğrulama

Tüm fonksiyonlar Swagger arayüzü üzerinden test edilmiştir.

HTTP 200 / 201 dönüşleri alınmıştır

Analitik endpoint’ler doğru çıktılar üretmektedir

Ekran görüntüleri ek olarak sunulabilir

7. Agile & Scrum Yaklaşımı

Proje, User Story’ler üzerinden planlanmıştır

Önceliklendirme yapılmıştır

Analitik özellikler iteratif olarak eklenmiştir

Her önemli aşama ayrı commit ile ilerletilmiştir

8. AI Kullanımı Hakkında Açıklama

Bu projede yapay zekâ, tasarım ve analiz aşamasında destekleyici (AI-assisted) olarak kullanılmıştır.
Kodlama, hata ayıklama ve doğrulama süreçleri geliştirici tarafından gerçekleştirilmiştir.

9. Sonuç

Geliştirilen sistem, klasik görev yönetim uygulamalarından farklı olarak karar destek mekanizmaları içermekte ve kullanıcıya proaktif öneriler sunmaktadır.
Bu yönüyle proje, yazılım proje yönetimi ve analitik düşünce hedeflerini başarıyla karşılamaktadır.
