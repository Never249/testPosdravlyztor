using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace testPosdravlyztor
{

    class Program
    {
        public static List<Birthday> Birthdays = new List<Birthday>();
        static char Chase;
       static void Menu()
        {
            
            Console.Write("\nВыберет нужное действие\n" + "1 Добавление дня рождения в список\n" + "2 Удаление дня рождения из списка\n" + "3 Редактирование записи о дне рождения\n" + "4 Вывод всех дней рождения\n" + "5 Вывод ближжайших дней рождений \n" + "0 Для выхода из приложения \n");
            Chase = Console.ReadKey().KeyChar;
            
        }
        static void Main(string[] args)
        {
            Open();
            ReadCloserBD();
            Menu();
            while (Chase!='0')
            {
                switch (Chase)
                {
                    case '1':
                        Console.Clear();
                        RecordBD();
                        
                        break;
                    case '2':
                        Console.Clear();
                        DeliteBD();
                       
                        break;
                    case '3':
                        Console.Clear();
                        ChangeBD();

                        break;
                    case '4':
                        Console.Clear();
                        ReadAllBD();
                        break;
                    case '5':
                        Console.Clear();
                        ReadCloserBD();
                        break;
                    case '0':
                    break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Введён неверный символ");
                        break;
                }
                Console.Clear();

                Menu();
            }
            
           
         
            

            }
        static void Open()
        {
            

            
                if (!File.Exists("BD.bin"))
                {
                    return;
                }
                var formatter = new BinaryFormatter();

                using (var stream = File.OpenRead("BD.bin"))
                {
                    Birthdays = formatter.Deserialize(stream) as List<Birthday>;
                }

            
        }
        static void ReadCloserBD()
        {

            DateTime Now = DateTime.Now;
            Now=Now.AddDays(-1);
            DateTime Closer = Now.AddDays(30);
            for(int i=0;i<Birthdays.Count;i++)
            {
                if (Birthdays[i].dateBD>=Now && Birthdays[i].dateBD<=Closer)
                {
                    Console.WriteLine($"--------------------\n №{i}\n{Birthdays[i].FSName}\n день рождения {Birthdays[i].dateBD}\n--------------------\n");
                }
            }
            
           
            Console.WriteLine("Нажмити на любой символ чтоы продолжить ");
            Console.ReadKey();
        }
        static void ReadAllBD()
        {
            Open();
            for (int i = 0; i < Birthdays.Count; i++)
            {
                Console.WriteLine($"--------------------\n №{i}\n{Birthdays[i].FSName}\n день рождения {Birthdays[i].dateBD}\n--------------------\n");
            }
            Console.WriteLine("Нажмити на любой символ чтоы продолжить ");
            Console.ReadKey();
          
        }
        static void SaveChange()
        {
            var formatter = new BinaryFormatter();

            using (var stream = File.OpenWrite("BD.bin"))
            {
                formatter.Serialize(stream, Birthdays);
            }
        }
        static void RecordBD()
        {
            DateTime DT;
            string date;
            Birthday BD = new Birthday();
            Console.WriteLine("Введите имя и фамилию");
            BD.FSName = Console.ReadLine();

            do
            {
                Console.WriteLine("Введите день и месяц рождения (в случии неверного ввода запрос произойдёт заного )");
                date = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(date, "dd.MM", null, DateTimeStyles.None, out DT));
            BD.dateBD = DT;
            Birthdays.Add(BD);
            SaveChange();
            Console.WriteLine("Нажмити на любой символ чтоы продолжить ");
            Console.ReadKey();




        }

        static void DeliteBD()
        {
            for (int i = 0; i < Birthdays.Count; i++)
            {
                Console.WriteLine($"--------------------\n №{i}\n{Birthdays[i].FSName}\n день рождения {Birthdays[i].dateBD}\n--------------------\n");


            }

            Console.WriteLine("Введите номер для удаления");
            int key = Convert.ToInt32(Console.ReadLine());
            try { Birthdays.RemoveAt(key); }
            catch (Exception) { Console.Write("Ошибка\n"); }
            SaveChange();
            Console.WriteLine("Нажмити на любой символ чтоы продолжить ");
            Console.ReadKey();
        }

        static void ChangeBD()
        {
            for (int i = 0; i < Birthdays.Count; i++)
            {
                Console.WriteLine($"--------------------\n №{i}\n{Birthdays[i].FSName}\n день рождения {Birthdays[i].dateBD}\n--------------------\n");
            }

            Console.WriteLine("Ввежите номер для изменения");
            int key = Convert.ToInt32(Console.ReadLine());
            try
            {
                DateTime DT;
                string date;
                Birthday BD = new Birthday();
                Console.WriteLine("Введите имя и фамилию");
                Birthdays[key].FSName = Console.ReadLine();

                do
                {
                    Console.WriteLine("Введите день и месяц рождения (в случии неверного ввода запрос произойдёт заного )");
                    date = Console.ReadLine();
                }
                while (!DateTime.TryParseExact(date, "dd.MM", null, DateTimeStyles.None, out DT));
                Birthdays[key].dateBD = DT;

            }
            catch (Exception) { Console.Write("Ошибка\n"); }
            SaveChange();
            Console.WriteLine("Нажмити на любой символ чтоы продолжить ");
            Console.ReadKey();


        }
    }
    }
