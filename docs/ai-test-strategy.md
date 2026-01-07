# AI-Assisted Test Senaryoları ve Edge-Case Analizi

Bu adım iki ana bölümden oluşmaktadır:

1. **Test senaryolarının belirlenmesi (senaryo bazlı testler)**
2. **Edge-case (sınır durum) doğrulaması**

> Not: Ders kapsamında **Swagger arayüzü kullanılarak yapılan manuel doğrulamalar** test süreci için yeterli kabul edilmektedir. Unit test yazımı zorunlu değildir.

---

## 4.1 Test Yaklaşımı

Bu projede test süreci, **ChatGPT desteğiyle oluşturulan test senaryoları ve edge-case önerileri** üzerinden planlanmıştır.  
Belirlenen senaryolar doğrultusunda, uygulamanın fonksiyonel doğrulaması **Swagger UI** aracılığıyla manuel olarak gerçekleştirilmiştir.

Test sürecinde özellikle:

- Görev oluşturma ve güncelleme işlemleri
- Analitik endpoint’lerin doğru risk ve öneri üretmesi
- Hatalı veya sınır durumlarda sistemin stabil çalışması

gibi kriterler doğrulanmıştır.

---

## 4.2 Test Senaryoları

Aşağıdaki tabloda, projede uygulanan temel test senaryoları yer almaktadır:

| Test ID | Endpoint                                | Senaryo                                  | Beklenen Sonuç                            | Gerçek Sonuç |
| ------- | --------------------------------------- | ---------------------------------------- | ----------------------------------------- | ------------ |
| TS-01   | `POST /api/Tasks`                       | Geçerli görev ekleme                     | HTTP 201, görev başarıyla kaydedilir      | ✔️ Başarılı  |
| TS-02   | `PUT /api/Tasks/{id}`                   | Görev durumu `Done` yapılır              | Görev durumu güncellenir                  | ✔️ Başarılı  |
| TS-03   | `GET /api/Analytics/delay-risk`         | Deadline yakın ve yüksek öncelikli görev | Yüksek risk skoru                         | ✔️ Başarılı  |
| TS-04   | `GET /api/Analytics/priority-conflicts` | Aynı güne ait 2+ yüksek öncelikli görev  | `HasConflict = true`                      | ✔️ Başarılı  |
| TS-05   | `GET /api/Analytics/recommendations`    | Bağımlı görevi olan görev                | “Önce bağımlı görev tamamlanmalı” önerisi | ✔️ Başarılı  |

> Bu test senaryoları **ChatGPT tarafından önerilmiş**, ancak **uygulama ve doğrulama süreci ekip tarafından manuel olarak gerçekleştirilmiştir**.

---

## 4.3 Edge-Case Analizi

Edge-case analizleri, sistemin beklenmeyen veya sınır durumlarda nasıl davrandığını doğrulamak amacıyla yapılmıştır.

### 🔹 Edge-Case 1 — Döngüsel Bağımlılık

- **Durum:** A → B → A şeklinde döngüsel görev bağımlılığı
- **Beklenen:** Sistem döngüsel bağımlılığa izin vermemelidir
- **Sonuç:** Döngüsel bağımlılık engellenmiştir

### 🔹 Edge-Case 2 — Tamamlanmış Görevlerin Analizi

- **Durum:** `Status = Done` olan görev
- **Beklenen:** Risk ve öneri analizlerine dahil edilmemelidir
- **Sonuç:** Görev analiz dışı bırakılmıştır

### 🔹 Edge-Case 3 — Çok Yakın Deadline

- **Durum:** Deadline süresi 24 saatten az olan görev
- **Beklenen:** “Acil” uyarısı ve yüksek öncelikli öneri
- **Sonuç:** Uygun uyarı ve öneri başarıyla üretilmiştir

### 🔹 Edge-Case 4 — Görev Bulunmayan Kullanıcı

- **Durum:** Kullanıcının sisteme kayıtlı hiç görevi yok
- **Beklenen:** Boş liste dönülmesi, hata oluşmaması
- **Sonuç:** Sistem stabil şekilde boş response döndürmüştür

---

## 4.4 AI-Assisted Test Değerlendirmesi

Bu projede **ChatGPT**, test senaryolarının oluşturulması ve olası edge-case durumlarının öngörülmesi aşamasında destekleyici bir araç olarak kullanılmıştır.  
Ancak tüm testlerin uygulanması, çıktılarının yorumlanması ve doğrulanması ekip tarafından manuel olarak gerçekleştirilmiştir.

Bu yaklaşım, yapay zekânın yazılım test süreçlerinde **yardımcı ve hızlandırıcı bir araç** olarak etkili şekilde kullanılabileceğini; ancak **nihai karar ve sorumluluğun geliştiricide olması gerektiğini** açıkça ortaya koymaktadır.
