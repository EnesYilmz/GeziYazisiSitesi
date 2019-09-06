# Gezi Yazısı Sitesi

ASP.NET Core MVC 2 ile yaptığımız gezi yazısı paylaşma sistemi. 
Kendimizi geliştirmek için arkadaşım Emirhan ile beraber yaptık.

## Kurulum

Projeyi indirip Visual Studio ile açtıktan sonra aşağıdaki komut ile veritabanını oluşturabilirsiniz.

```sh
dotnet ef database update
```

Veritabanı olarak MSSQL kullanmalısınız. Veritabanınızın connection string'ini appsettings.json adlı dosyaya yazabilirsiniz.

## Kullanım

Projeyi çalıştırdıktan sonra kayıt olup üye girişi yaptıktan sonra gezi yazısı yazabilir, yazılara yorum ve beğeni atabilirsiniz.
Admin siteye yeni ülke ve şehirler ekleyebilir ayrıca siteye gönderilen yazıları yayınlanması için onaylayabilir.

## Meta

Enes Yılmaz – [@LinkedIn](https://www.linkedin.com/in/enes-ylmz/) – enes.yilmaz15@ogr.sakarya.edu.tr
[https://github.com/EnesYilmz]

Emirhan Ülker – [@LinkedIn](https://www.linkedin.com/in/emir-%C3%BClker-21b33416a/) – emirhan.ulker@ogr.sakarya.edu.tr
[https://github.com/Emrhnlkr]
