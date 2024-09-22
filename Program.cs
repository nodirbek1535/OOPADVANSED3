using System.IO;
using System.Text.Json;
using Kitobxonlar.Classes;
using Book.Classes;
Console.WriteLine("kutubxona dasturiga xush kelibsiz loginingizni kiriting:");
Kitobxon kitobxon = new Kitobxon("","",0,"","",""); // Foydalanuvchi obyektini yaratish
kitobxon.kirish();  // Foydalanuvchidan login olish

// `kirish()` metodi ichida foydalanuvchi logini olingan bo'lsa, endi foydalanuvchi sessiyasini davom ettirish

// Foydalanuvchilar ro'yxatini yuklash (kitobxonlar.json)
string foydalanuvchilarJson = File.ReadAllText("kitobxonlar.json");
List<Kitobxon> foydalanuvchilar = JsonSerializer.Deserialize<List<Kitobxon>>(foydalanuvchilarJson);
Boook book = new Boook("","","","",0,0,new List<string> { "Alisher", "Jamshid", "Nigora" }, new List<string> { "Farrukh", "Madina" });
// Foydalanuvchini login bo'yicha topish
Kitobxon foydalanuvchi = foydalanuvchilar.FirstOrDefault(f => f.Login == kitobxon.Login); // Foydalanuvchi loginini ishlatish

if (foydalanuvchi != null)
{
    Console.WriteLine("Xush kelibsiz, " + foydalanuvchi.Ism);

    Console.WriteLine("menu ni korish uchun 1 ni kiriting");
    string input = Console.ReadLine();

    if (input == "1")
    {
        kitobxon.menu();  // Foydalanuvchi uchun menyu ko'rsatish
        int a = int.Parse(Console.ReadLine());

        switch (a)
        {
            case 1:
                // Kitoblar ro'yxatini yuklash (kitoblarroyxati.json)
                string filePath = "kitoblarroyxati.json";
                string jsonData = File.ReadAllText(filePath);
                List<Boook> kitoblar = JsonSerializer.Deserialize<List<Boook>>(jsonData);

                // Kitoblarni ekranga chiqarish
                foreach (var kitob in kitoblar)
                {
                    kitob.kitoblarnichiqarish();
                }

                // Kitobni ijaraga olish jarayoni
                
                book.ijaragaolish(kitoblar, foydalanuvchilar, foydalanuvchi);  // Sessiyadagi foydalanuvchidan foydalanish
                break;
            
            case 2:
                 string kitoblarJson = File.ReadAllText("kitoblarroyxati.json");
                List<Boook> kitoblar1 = JsonSerializer.Deserialize<List<Boook>>(kitoblarJson);

                // Kitobni qaytarish jarayoni
                book.kitobniQaytarish(kitoblar1,foydalanuvchilar, foydalanuvchi);
            break;

            default:
                Console.WriteLine("Noto'g'ri tanlov.");
            break;
        }
    }
}
else
{
    Console.WriteLine("Foydalanuvchi topilmadi.");
}