# Veri Modeli (Data Model)

Bu projede kullanıcıya özel görev yönetimi sağlamak amacıyla
aşağıdaki temel varlıklar (entity) tanımlanmıştır.

Bu veri modeli ChatGPT kullanılarak AI-assisted şekilde önerilmiş,
nihai yapı insan tarafından gözden geçirilerek sadeleştirilmiş ve
proje kapsamına uygun hale getirilmiştir.

---

## User

Sistemi kullanan kişileri temsil eder.

Alanlar:

- Id
- Email
- PasswordHash
- CreatedAt

---

## Task

Kullanıcıya ait görevleri temsil eder.

Alanlar:

- Id
- UserId
- Title
- Description
- Priority (Low / Medium / High)
- EstimatedDuration (saat veya gün)
- Deadline
- Status (Todo / InProgress / Done)
- CreatedAt

---

## TaskDependency

Görevler arası bağımlılık ilişkisini temsil eder.

Alanlar:

- Id
- UserId
- TaskId (bağımlı görev)
- DependsOnTaskId (önce tamamlanması gereken görev)

Bir görev, kendisinden önce tamamlanması gereken başka bir göreve
bağlanabilir. Döngüsel bağımlılıklara (A → B → A) izin verilmez.

## Mimari Karar Açıklaması (AI vs İnsan)

AI, görev bağımlılıklarının Task tablosu içinde tutulmasını önermiştir.
Ancak bu yaklaşım, bir görevin birden fazla göreve bağımlı olması
durumunda veri tekrarına ve karmaşaya yol açabileceğinden reddedilmiştir.

Bunun yerine bağımlılıkların ayrı bir TaskDependency tablosunda
tutulmasına karar verilmiştir. Bu yapı daha esnek, genişletilebilir
ve döngüsel bağımlılık kontrollerinin yapılmasını kolaylaştırmaktadır.

Bu karar insan müdahalesiyle verilmiştir.
