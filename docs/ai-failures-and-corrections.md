ChatGPT Yanılgıları, Hatalı Öneriler

Bu projede ChatGPT, analizden geliştirmeye ve hata ayıklamaya kadar birçok aşamada ekibe destek olmuştur. Ancak yazılım geliştirme sürecinde ChatGPT tarafından sunulan öneriler her zaman bağlama tam olarak uymamış; bazı durumlarda eksik veya hatalı yönlendirmeler içermiştir. Bu tür durumlarda ekip, ChatGPT’den tekrar destek alarak, elde edilen önerileri sorgulamış ve nihai çözümü insan denetimiyle birlikte şekillendirmiştir.

Aşağıda, proje sürecinde karşılaşılan bu durumlara ilişkin örnekler sunulmaktadır.

NuGet Kaynağı Yetkilendirme Hatası (401 Unauthorized)

Proje başlangıcında, Entity Framework Core bağımlılıklarının kurulumu için ChatGPT tarafından varsayılan NuGet yapılandırmasının kullanılması önerilmiştir. Ancak proje ortamında daha önce tanımlı olan Azure DevOps private NuGet feed’i nedeniyle dotnet restore işlemi 401 yetkilendirme hatası ile başarısız olmuştur.

Bu noktada ekip, problemi analiz etmek amacıyla ChatGPT’den tekrar yardım almış; NuGet source’ların listelenmesi ve yetkisiz kaynakların devre dışı bırakılması yönünde öneriler almıştır. Bu öneriler ekip tarafından uygulanmış ve yalnızca nuget.org kaynağı aktif bırakılarak bağımlılıklar başarıyla yüklenmiştir.

Sonuç olarak, ChatGPT’nin ilk önerisi bağlamdan bağımsız olsa da, tekrar edilen ChatGPT destekli analiz sayesinde problem çözülmüş ve uygulama sorunsuz şekilde derlenmiştir.

OpenAPI / Swagger Paket Uyumsuzluğu

ChatGPT tarafından varsayılan OpenAPI ve Swagger yapılandırmasının kullanılması önerilmiş, ancak çalışma zamanında Microsoft.OpenApi paket sürümleri arasında uyumsuzluk meydana gelmiştir. Bu durum uygulamanın TypeLoadException hatası ile çökmesine neden olmuştur.

Ekip, hata mesajlarını analiz ettikten sonra ChatGPT’den tekrar destek alarak sorunun paket uyumsuzluğundan kaynaklandığını tespit etmiştir. Alınan yönlendirmeler doğrultusunda Swagger/OpenAPI yapılandırması sadeleştirilmiş ve gereksiz servisler kaldırılmıştır.

Bu müdahale sonucunda API tekrar ayağa kalkmış ve Swagger arayüzü stabil şekilde kullanılabilir hale gelmiştir.

dotnet-ef Aracının Kurulamaması

Veritabanı işlemleri için ChatGPT tarafından dotnet-ef aracının kurulması önerilmiştir. Ancak kullanılan .NET sürümü ve proje yapılandırması nedeniyle bu araç yüklenememiş ve migration komutları çalışmamıştır.

Bu aşamada ekip, ChatGPT ile birlikte alternatif yaklaşımları değerlendirmiştir. Yapılan değerlendirme sonucunda, proje kapsamında migration kullanımının zorunlu olmadığı anlaşılmış ve SQLite kullanımı sade bir yapı ile devam ettirilmiştir. Böylece gereksiz karmaşıklık önlenmiş ve proje gereksinimleri etkilenmeden geliştirmeye devam edilmiştir.

TaskStatus Enum İsim Çakışması

Görev durumlarını temsil etmek amacıyla TaskStatus enum’u tanımlanmış, ancak bu isim .NET içerisindeki System.Threading.Tasks.TaskStatus ile çakışmıştır. ChatGPT tarafından ilk aşamada bu çakışma öngörülememiştir.

Derleme hatası sonrasında ekip, ChatGPT’den destek alarak namespace çakışmasının giderilmesi gerektiğini belirlemiş ve enum kullanımında Models.TaskStatus şeklinde açık namespace tanımı yapılmıştır. Bu sayede hata giderilmiş ve kod okunabilirliği korunmuştur.

JSON Serialization Döngü (Cycle) Hatası

Başlangıçta ChatGPT tarafından entity nesnelerinin doğrudan API response olarak döndürülmesi önerilmiştir. Ancak TaskItem ve TaskDependency arasındaki çift yönlü ilişki nedeniyle JSON serialization sırasında döngü oluşmuştur.

Bu kritik hata sonrasında ekip, ChatGPT’den tekrar destek alarak çözüm alternatiflerini değerlendirmiştir. Alınan öneriler doğrultusunda DTO ve anonim nesneler kullanılmış, response yapıları sadeleştirilmiştir. Böylece döngü problemi ortadan kaldırılmış ve API istemcileri için güvenli bir veri dönüşü sağlanmıştır.

Bu örnek, ChatGPT önerilerinin her zaman production ortamı için yeterli olmadığını; ancak iteratif ChatGPT destekli hata ayıklama yaklaşımıyla etkin şekilde düzeltilebildiğini göstermektedir.

Genel Değerlendirme

Bu örnekler, ChatGPT’nin yazılım geliştirme sürecinde güçlü bir destek aracı olduğunu; ancak bağlamsal farkındalık ve eleştirel insan denetimi olmadan tek başına yeterli olmadığını göstermektedir. Projede, ChatGPT önerileri yalnızca ilk yönlendirme olarak ele alınmış; hata ayıklama ve nihai karar süreçlerinde ekip, ChatGPT’den tekrar destek alarak en uygun çözümü üretmiştir. Bu yaklaşım, ChatGPT’nin AI-assisted yazılım geliştirme sürecindeki etkin ve etik kullanımını temsil etmektedir.
