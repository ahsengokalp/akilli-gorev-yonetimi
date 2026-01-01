# Gereksinim Analizi – Kullanıcı Hikâyeleri

Bu bölümde yer alan kullanıcı hikâyeleri,
ChatGPT kullanılarak AI-assisted şekilde üretilmiştir.
Hikâyeler daha sonra insan tarafından gözden geçirilmiş
ve proje kapsamına uygunluğu kontrol edilmiştir.

USER STORY 1 – Kullanıcıya Özel Görev Alanı
User Story: Bir kullanıcı olarak, yalnızca bana ait görevleri görebileceğim kişisel bir alan istiyorum,
böylece görevlerim başkalarının görevleriyle karışmasın.
Acceptance Criteria :Kullanıcı giriş yaptıktan sonra yalnızca kendi görevlerini görmelidir. Başka kullanıcıların görevlerine erişim olmamalıdır. Oturum kapatıldığında veriye erişim kesilmelidir
Öncelik: Yüksek
Tahmini Süre: 1 gün
Deadline: Proje 2. günü
Bağımlılık: Yok (temel yapı)

USER STORY 2 – Görev Ekleme

User Story

Bir kullanıcı olarak yeni bir görev eklemek istiyorum,
böylece yapmam gereken işleri sisteme kaydedebilirim.

Acceptance Criteria

Görev adı boş bırakılamaz

Öncelik (düşük/orta/yüksek) seçilmelidir

Tahmini süre (saat/gün) girilebilmelidir

Son teslim tarihi zorunlu olmalıdır

Görev başarıyla kaydedildiğinde listede görünmelidir

Öncelik: Yüksek
Tahmini Süre: 1 gün
Deadline: Proje 3. günü
Bağımlılık: User Story 1

📌 USER STORY 3 – Görev Güncelleme

User Story

Bir kullanıcı olarak mevcut görevlerimi güncellemek istiyorum,
böylece değişen koşullara göre planımı revize edebilirim.

Acceptance Criteria

Görev adı, öncelik, süre ve teslim tarihi güncellenebilmelidir

Değişiklikler anında kaydedilmelidir

Güncelleme sonrası analizler yeniden çalıştırılmalıdır

Öncelik: Orta
Tahmini Süre: 0.5 gün
Deadline: Proje 4. günü
Bağımlılık: User Story 2

📌 USER STORY 4 – Görev Silme

User Story

Bir kullanıcı olarak artık gerekli olmayan görevleri silmek istiyorum,
böylece görev listem sade kalır.

Acceptance Criteria

Silme öncesi kullanıcıdan onay alınmalıdır

Silinen görev listeden tamamen kaldırılmalıdır

Silinen görev başka bir görevin bağımlılığıysa uyarı verilmelidir

Öncelik: Orta
Tahmini Süre: 0.5 gün
Deadline: Proje 4. günü
Bağımlılık: User Story 2

📌 USER STORY 5 – Görevler Arası Bağımlılık Tanımlama

User Story

Bir kullanıcı olarak görevler arasında bağımlılık tanımlamak istiyorum,
böylece bir görev bitmeden diğeri başlamasın.

Acceptance Criteria

Bir görev, başka bir göreve bağlanabilmelidir

Bağımlı görev, önceki görev tamamlanmadan “aktif” olamamalıdır

Döngüsel bağımlılığa izin verilmemelidir (A → B → A)

Öncelik: Yüksek
Tahmini Süre: 1 gün
Deadline: Proje 5. günü
Bağımlılık: User Story 2

📌 USER STORY 6 – Gecikme Riski Analizi

User Story

Bir kullanıcı olarak, gecikme riski olan görevlerimi görmek istiyorum,
böylece zamanında aksiyon alabilirim.

Acceptance Criteria

Sistem, teslim tarihi yaklaşan ve süresi uzun görevleri analiz etmelidir

Gecikme riski olan görevler görsel olarak işaretlenmelidir (renk/ikon)

Analiz otomatik çalışmalıdır

Öncelik: Yüksek
Tahmini Süre: 1 gün
Deadline: Proje 6. günü
Bağımlılık: User Story 2, 5

📌 USER STORY 7 – Öncelik Çakışması Analizi

User Story

Bir kullanıcı olarak aynı zaman aralığında birden fazla yüksek öncelikli görevim olduğunda uyarılmak istiyorum,
böylece planımı daha gerçekçi yapabilirim.

Acceptance Criteria

Aynı zaman diliminde çakışan yüksek öncelikli görevler tespit edilmelidir

Kullanıcıya çakışma bildirimi gösterilmelidir

Çakışan görevler listelenmelidir

Öncelik: Orta
Tahmini Süre: 0.5 gün
Deadline: Proje 6. günü
Bağımlılık: User Story 2

📌 USER STORY 8 – Yapay Zekâ Destekli Yeniden Önceliklendirme Önerisi

User Story

Bir kullanıcı olarak sistemin bana görevlerimi nasıl yeniden planlayabileceğime dair öneri sunmasını istiyorum,
böylece daha verimli çalışabilirim.

Acceptance Criteria

Sistem en az 1 planlama veya öncelik değişikliği önerisi üretmelidir

Öneri, gecikme ve öncelik analizlerine dayanmalıdır

Kullanıcı öneriyi kabul veya reddedebilmelidir

Öncelik: Yüksek
Tahmini Süre: 1–1.5 gün
Deadline: Proje 7. günü
Bağımlılık: User Story 6, 7

📌 USER STORY 9 – Görev Durumu Takibi

User Story

Bir kullanıcı olarak görevlerimi “yapılacak / devam ediyor / tamamlandı” olarak işaretlemek istiyorum,
böylece ilerlememi net görebileyim.

Acceptance Criteria

Görev durumu değiştirilebilmelidir

Tamamlanan görevler bağımlı görevlerin kilidini açmalıdır

Tamamlanan görevler analizlerde dikkate alınmamalıdır

Öncelik: Orta
Tahmini Süre: 0.5 gün
Deadline: Proje 5. günü
Bağımlılık: User Story 5
