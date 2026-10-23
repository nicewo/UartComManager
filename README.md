# UART / HID Communication Manager

<p align="center">
  <strong>Seri port ve USB HID cihazları için Windows iletişim aracı</strong><br>
  WPF arayüzü üzerinden COM port seçimi, seri veri gönderme ve gelen veriyi izleme.
</p>

<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8.0%20Windows-512BD4?logo=dotnet&logoColor=white">
  <img alt="UI" src="https://img.shields.io/badge/UI-WPF-0078D4?logo=windows&logoColor=white">
  <img alt="UART" src="https://img.shields.io/badge/Communication-UART%20%2F%20COM-008080">
  <img alt="HID" src="https://img.shields.io/badge/USB-HID-555555">
</p>

---

## Genel Bakış

UartComManager, Windows masaüstünde seri port ve HID cihazlarıyla çalışmak için hazırlanmış bir C# WPF uygulamasıdır. Kodda COM portlarını listeleme, baud rate seçme, bağlantı açıp kapatma, seri veri gönderme ve gelen veriyi arayüzde gösterme işlevleri bulunur. HID cihazlarını listeleyen bir yardımcı bölüm de yer alır.

## Özellikler

- 🔌 Kullanılabilir COM portlarını listeleme
- ⚙️ 9600–115200 aralığında yaygın baud rate seçenekleri
- 📤 Seri porta metin gönderme
- 📥 Gelen seri veriyi canlı görüntüleme
- 📜 Otomatik kaydırma ve alıcı ekranını temizleme
- 🔎 USB HID cihazlarını VID/PID ve açıklama ile listeleme
- ⚠️ Seri port bağlantısı kesildiğinde uyarı gösterme

## Teknolojiler

- C# / .NET 8 for Windows
- WPF
- System.IO.Ports
- HidLibrary.Unofficial
- System.Management

## Derleme

1. Windows üzerinde .NET 8 SDK kurun.
2. Visual Studio'da UartComManager.sln dosyasını açın.
3. NuGet paketlerini geri yükleyin.
4. Release yapılandırmasında derleyin.

Komut satırı:

- dotnet restore .\\UartComManager.sln
- dotnet build .\\UartComManager.sln -c Release

## Kullanım

1. Uygulamayı açın ve listeden COM portunu seçin.
2. Cihazınızın seri haberleşme ayarına uygun baud rate değerini seçin.
3. Bağlan düğmesiyle portu açın.
4. Gönderme alanından test verisi gönderin; gelen veri alıcı ekranında görüntülenir.
5. HID cihaz listeleme özelliğini kullanacağınız durumda hedef cihazın Windows tarafından algılandığını doğrulayın.

## Notlar

- Baud rate dışında parity, data bits, stop bits ve terminator gibi ayarların cihaz gereksinimlerine göre eklenmesi gerekebilir.
- Seri porttan gelen veri parçalı gelebilir; uygulama şu an gelen parçaları metin olarak ekrana aktarır. Protokol tabanlı kullanımda paket ayrıştırma ve timeout mantığı ekleyin.
- HID cihaz listeleme altyapısı bulunsa da mevcut ana ekran akışında HID bağlantı/transfer fonksiyonlarının tamamı uygulanmış olmayabilir.

## Lisans

Depoda ayrı bir lisans belirtilmemişse, kodun yeniden kullanımı için depo sahibinden izin alınmalıdır.
