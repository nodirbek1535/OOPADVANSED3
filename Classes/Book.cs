using System.Globalization;
using System.Text.Json;
using Kitobxonlar.Classes;

namespace Book.Classes
{
    class Boook
    {
        public string Bookid{get ; set ;}
        public string Bookname{get ; set ;}
        public string Bookjanr{get ; set ;}
        public string Bookmualif{get ; set ;}
        public uint Bookjamimiqdor{get ; set ;}
        public uint Bookqollardagisoni{get ; set ;}
        public List<string> KitobxonlarRoyxati { get; set; }=new List<string>();
        public List<string> Bookoquvchilar{get ; set ;}=new List<string>();
        public Boook(string bookid,string bookname,string bookjanr,string bookmualif,uint bookjamimiqdor,uint bookqollardagisoni, List<string> kitobxonlarRoyxati,List<string> bookoquvchilar )
        {
            Bookid=bookid;
            Bookname=bookname;
            Bookjanr=bookjanr;
            Bookmualif=bookmualif;
            Bookjamimiqdor=bookjamimiqdor;
            Bookqollardagisoni=bookqollardagisoni;
            KitobxonlarRoyxati=kitobxonlarRoyxati;
            Bookoquvchilar=bookoquvchilar;

        }
        public void kitoblarnichiqarish()
        {
            
            string oquvchilar = KitobxonlarRoyxati != null ? string.Join(", ", KitobxonlarRoyxati) : "Kitobxonlar mavjud emas";
            uint kutubxonadagiKitoblar = Bookjamimiqdor - Bookqollardagisoni;

            Console.WriteLine($"Kitob ID: {Bookid}");
            Console.WriteLine($"Nomi: {Bookname}");
            Console.WriteLine($"Janri: {Bookjanr}");
            Console.WriteLine($"Avtori: {Bookmualif}");
            Console.WriteLine($"Jami Miqdori: {Bookjamimiqdor}");
            Console.WriteLine($"Kutubxonada Qolgan Miqdori: {kutubxonadagiKitoblar}");
            Console.WriteLine($"Kitobxonlar: {oquvchilar}");
            Console.WriteLine("---------------------------");
        }

        public void ijaragaolish(List<Boook> kitoblar, List<Kitobxon> foydalanuvchilar, Kitobxon foydalanuvchi)
        {
            Console.WriteLine("Olmoqchi bo'lgan kitobingizni IDsini kiriting:");
            string aaa = Console.ReadLine();

            // Kitobni ID bo'yicha qidirish
            Boook tanlanganKitob = kitoblar.FirstOrDefault(k => k.Bookid == aaa);

            if (tanlanganKitob == null)
            {
                Console.WriteLine("Bunday ID bilan kitob topilmadi.");
                return;
            }

            // Kitobxonlar ro'yxati null bo'lsa, yangi bo'sh ro'yxat yaratish
            if (tanlanganKitob.KitobxonlarRoyxati == null)
            {
                tanlanganKitob.KitobxonlarRoyxati = new List<string>();
            }

            // Agar foydalanuvchining o'qiyotgan kitoblari ro'yxati null bo'lsa, uni yaratish
            if (foydalanuvchi.Oqiyotgankitoblar == null)
            {
                foydalanuvchi.Oqiyotgankitoblar = new List<string>();
            }

            // Kutubxonadagi kitoblar sonini to'g'ri hisoblash
            uint kutubxonadagiKitoblar = tanlanganKitob.Bookjamimiqdor - tanlanganKitob.Bookqollardagisoni;

            // Agar kutubxonada qolgan kitoblar soni > 0 bo'lsa
            if (kutubxonadagiKitoblar > 0)
            {
                tanlanganKitob.Bookqollardagisoni++;  // Qolgan kitoblar sonini kamaytirish
                tanlanganKitob.KitobxonlarRoyxati.Add(foydalanuvchi.Ism);  // Kitobxonlar ro'yxatiga foydalanuvchi ismni qo'shish
                foydalanuvchi.Oqiyotgankitoblar.Add(tanlanganKitob.Bookname);  // Foydalanuvchi o'qiyotgan kitoblar ro'yxatiga qo'shish

                Console.WriteLine($"Kitob: {tanlanganKitob.Bookname} olindi, kutubxonada: {tanlanganKitob.Bookjamimiqdor-tanlanganKitob.Bookqollardagisoni} ta qoldi.");

                // JSON sozlamalari, null qiymatlarni yozmaslik
                var options = new JsonSerializerOptions 
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };

                // Yangilangan kitoblar ro'yxatini JSON formatida qayta yozish
                string updatedKitoblarJson = JsonSerializer.Serialize(kitoblar, options);
                File.WriteAllText("kitoblarroyxati.json", updatedKitoblarJson);

                // Foydalanuvchi ma'lumotlarini yangilash va qayta yozish
                string updatedFoydalanuvchilarJson = JsonSerializer.Serialize(foydalanuvchilar, options);
                File.WriteAllText("kitobxonlar.json", updatedFoydalanuvchilarJson);
            }
            else
            {
                Console.WriteLine($"Kechirasiz, {tanlanganKitob.Bookname} dan qolmadi.");
            }
        }

        public void kitobniQaytarish(List<Boook> kitoblar, List<Kitobxon> foydalanuvchilar, Kitobxon foydalanuvchi)
        {
            Console.WriteLine("Qaysi kitobni qaytarmoqchisiz? Kitob nomini kiriting:");
            string qaytarilayotganKitobNomi = Console.ReadLine();

            // Foydalanuvchining o'qiyotgan kitoblaridan kitobni qidiramiz
            if (foydalanuvchi.Oqiyotgankitoblar.Contains(qaytarilayotganKitobNomi))
            {
                // Kutubxonadan kitobni topish
                Boook qaytarilayotganKitob = kitoblar.FirstOrDefault(k => k.Bookname == qaytarilayotganKitobNomi);

                if (qaytarilayotganKitob != null)
                {
                    // Kutubxonadagi kitoblar sonini oshirish
                    qaytarilayotganKitob.Bookqollardagisoni++;

                    // O'qiyotgan kitoblar ro'yxatidan olib tashlash
                    foydalanuvchi.Oqiyotgankitoblar.Remove(qaytarilayotganKitobNomi);

                    // O'qib tugatgan kitoblar ro'yxatiga qo'shish
                    if (foydalanuvchi.Oqibtugatgankitoblar == null)
                    {
                        foydalanuvchi.Oqibtugatgankitoblar = new List<string>();
                    }
                    foydalanuvchi.Oqibtugatgankitoblar.Add(qaytarilayotganKitobNomi);

                    Console.WriteLine($"Siz {qaytarilayotganKitobNomi} kitobini qaytardingiz. Kutubxonada {qaytarilayotganKitob.Bookjamimiqdor-qaytarilayotganKitob.Bookqollardagisoni} ta mavjud.");

                    // Yangilangan foydalanuvchini ro'yxatdan topib, yangilash
                    int foydalanuvchiIndex = foydalanuvchilar.FindIndex(f => f.Login == foydalanuvchi.Login);
                    if (foydalanuvchiIndex >= 0)
                    {
                        foydalanuvchilar[foydalanuvchiIndex] = foydalanuvchi;  // Foydalanuvchini yangilash
                    }

                    // JSON fayllarni yangilash
                    string yangilanganKitoblarJson = JsonSerializer.Serialize(kitoblar, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText("kitoblarroyxati.json", yangilanganKitoblarJson);

                    string yangilanganFoydalanuvchilarJson = JsonSerializer.Serialize(foydalanuvchilar, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText("kitobxonlar.json", yangilanganFoydalanuvchilarJson);
                }
                else
                {
                    Console.WriteLine("Bunday kitob kutubxonada topilmadi.");
                }
            }
            else
            {
                Console.WriteLine("Siz bu kitobni o'qiyotganingiz yo'q.");
            }
        }
        
    }
}