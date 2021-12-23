using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MCD_CSVKullanimi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //fakedata ve csvhelper'i indirin.
            List<Musteri> musterilerim = new List<Musteri>();

            for (int i = 0; i < 50; i++)//fakedata dan 50 kayıt aldık.
            {
                Musteri temp = new Musteri();
                temp.ID = i.ToString();
                temp.Isim = FakeData.NameData.GetFirstName();
                temp.Soyisim = FakeData.NameData.GetSurname();
                temp.EmailAdres = ($"{temp.Isim}.{temp.Soyisim}@gmail.com");
                temp.TelefonNumara = FakeData.PhoneNumberData.GetPhoneNumber();
                musterilerim.Add(temp);

            }
            //csv dosya yazma işlemi

            StreamWriter sw = new StreamWriter(@"e:\csv\musteriler.csv");//klasörü kontrolü yapmadığımız için hangi sürücüye kaydediyorsanız o sürücüde "csv" klasörü oluşturunuz.
            CsvHelper.CsvWriter write = new CsvHelper.CsvWriter(sw, System.Globalization.CultureInfo.CurrentCulture);
            write.WriteHeader(typeof(Musteri));//musteri sınıfındaki başlıkları getirir.
            write.NextRecord();//alt satıra geçmek için kullanıldı.
            foreach (Musteri item in musterilerim)
            {
                write.WriteRecord(item);//her bir müşteriyi ekledik.
                write.NextRecord();
            }
            sw.Close();

            //CSV DOSYA OKUMA İŞLEMİ

            StreamReader Sr = new StreamReader(@"e:\CSV\Musteriler.csv");
            CsvHelper.CsvReader Reader = new CsvHelper.CsvReader(Sr,System.Globalization.CultureInfo.CurrentCulture);
            List<Musteri> OkunanData =Reader.GetRecords<Musteri>().ToList();
            foreach (var item in OkunanData)
            {
                Console.WriteLine(item.ID + " " +item.Isim + " " + item.EmailAdres + " " + item.TelefonNumara);
            }
            Sr.Close();
            Console.ReadKey();



        }
    }
}
