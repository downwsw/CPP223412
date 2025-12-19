using System;

namespace PoemCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            PoemManager manager = new PoemManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║   КОЛЕКЦІЯ ВІРШІВ - ГОЛОВНЕ МЕНЮ       ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. Додати вірш");
                Console.WriteLine("2. Видалити вірш");
                Console.WriteLine("3. Редагувати вірш");
                Console.WriteLine("4. Шукати вірш");
                Console.WriteLine("5. Показати всі вірші");
                Console.WriteLine("6. Зберегти колекцію у файл");
                Console.WriteLine("7. Завантажити колекцію з файлу");
                Console.WriteLine("8. Генерувати звіт");
                Console.WriteLine("9. Вихід");
                Console.WriteLine("════════════════════════════════════════");
                Console.Write("Виберіть дію: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            manager.AddPoem();
                            break;
                        case "2":
                            manager.DeletePoem();
                            break;
                        case "3":
                            manager.EditPoem();
                            break;
                        case "4":
                            manager.SearchPoems();
                            break;
                        case "5":
                            manager.ShowAllPoems();
                            break;
                        case "6":
                            manager.SaveToFile();
                            break;
                        case "7":
                            manager.LoadFromFile();
                            break;
                        case "8":
                            manager.GenerateReports();
                            break;
                        case "9":
                            running = false;
                            Console.WriteLine("До побачення!");
                            break;
                        default:
                            Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }
    }
}
