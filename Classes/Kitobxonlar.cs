using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Kitobxonlar.Classes
{
    class Kitobxon
    {
        public string Ism{get ; set ;}
        public string Familya {get ;set ;}
        public uint Yosh{get ;set ;}
        public string ID{get ; set;}
        public string Login{get ; set ;}
        public string Parol{get ; set ;}
        public List<string> Oqiyotgankitoblar{get ; set ;}=new List<string>();
        public List<string> Oqibtugatgankitoblar{ get ; set ;}=new List<string>();



        
        public Kitobxon(string ism,string familya,uint yosh,string id,string login,string parol)  
        {
            Ism=ism;
            Familya=familya;
            Yosh=yosh;
            ID=id;
            Login=login;
            Parol=parol;
        }

        List<Kitobxon> kitobxonlar=new List<Kitobxon>();





        public string  IDmetodi()
        {   
            string id="";
            Random random=new Random();
            for(int i=0;i<10;i++)
            {
                id+=random.Next(0,10);
            }
            return id;
        }




        string filepath="kitobxonlar.json";




        public void kirish()
        {
            Console.WriteLine("Loginni kiriting:");
            Login =Console.ReadLine();
            if (File.Exists(filepath))
            {
                string jsonString = File.ReadAllText(filepath);
                // Fayldan mavjud foydalanuvchilarni yuklash
                kitobxonlar = JsonSerializer.Deserialize<List<Kitobxon>>(jsonString) ?? new List<Kitobxon>();
            }
            else
            {
                // Agar fayl mavjud bo'lmasa, bo'sh ro'yxat yaratish
                kitobxonlar = new List<Kitobxon>();
            }
            bool isLoginExists=kitobxonlar.Any(r=>r.Login==Login);
            if(isLoginExists)
            {
                Console.WriteLine("Login topildi iltimos parolingizni kiriting:");
                string userpassword=Console.ReadLine();
                Kitobxon topilganKitobxon = kitobxonlar.FirstOrDefault(r => r.Login == Login);
                if (topilganKitobxon != null && topilganKitobxon.Parol == userpassword)
                {
                    Console.WriteLine("Parol to'g'ri, tizimga kirdingiz.");
                }
                else
                {
                    Console.WriteLine("Parol xato.");
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("siz rooyxatdan otmagansiz iltimos royxatdan oting.");
                Console.WriteLine("Ismingizni kiriting:");
                string username=Console.ReadLine();
                Console.WriteLine("Familyangizni kiriting");
                string usersurname=Console.ReadLine();
                Console.WriteLine("Yoshingizni kiriting");
                uint userage=uint.Parse(Console.ReadLine());
                Console.WriteLine("tizimga kirishingiz uchun loginingizni kiriting:");
                string userlogin=Console.ReadLine();
                Console.WriteLine("parol yarating:");
                string  userpassword=Console.ReadLine();
                string idd=IDmetodi();
                Console.WriteLine("sizning ID ingiz:"+idd);
                Kitobxon yangikitobxon=new Kitobxon(username,usersurname,userage,idd,userlogin,userpassword);
                kitobxonlar.Add(yangikitobxon);
                string yangiJsonString = JsonSerializer.Serialize(kitobxonlar, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filepath, yangiJsonString);
            }
        
        }






        public void menu()
        {
            Console.WriteLine("kitoblar royxatini korish uchun 1 ni kiriting ");
            Console.WriteLine("ozingizdagi kitobni topshirish uchun 2 ni kiriting");
        }
    }
}