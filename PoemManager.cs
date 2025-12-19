using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PoemCollection
{
    public class PoemManager
    {
        private List<Poem> poems;

        public PoemManager()
        {
            poems = new List<Poem>();
        }

        public void AddPoem()
        {
            Poem poem = new Poem();

            Console.Write("Введіть назву вірша: ");
            poem.Title = Console.ReadLine();

            Console.Write("Введіть ПІБ автора: ");
            poem.Author = Console.ReadLine();

            Console.Write("Введіть рік написання: ");
            poem.Year = int.Parse(Console.ReadLine());

            Console.Write("Введіть тему вірша: ");
            poem.Theme = Console.ReadLine();

            Console.WriteLine("Введіть текст вірша (для завершення введіть порожній рядок):");
            string text = "";
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
                text += line + "\n";
            }
            poem.Text = text.TrimEnd('\n');

            poems.Add(poem);
            Console.WriteLine("Вірш додано успішно!");
        }

        public void DeletePoem()
        {
            Console.Write("Введіть назву вірша для видалення: ");
            string title = Console.ReadLine();

            Poem poem = poems.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (poem != null)
            {
                poems.Remove(poem);
                Console.WriteLine("Вірш видалено!");
            }
            else
            {
                Console.WriteLine("Вірш не знайдено!");
            }
        }

        public void EditPoem()
        {
            Console.Write("Введіть назву вірша для редагування: ");
            string title = Console.ReadLine();

            Poem poem = poems.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (poem == null)
            {
                Console.WriteLine("Вірш не знайдено!");
                return;
            }

            Console.WriteLine("Поточна інформація:");
            poem.Display();

            Console.WriteLine("\nВиберіть що змінити:");
            Console.WriteLine("1. Назва");
            Console.WriteLine("2. Автор");
            Console.WriteLine("3. Рік");
            Console.WriteLine("4. Тема");
            Console.WriteLine("5. Текст");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Нова назва: ");
                    poem.Title = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("Новий автор: ");
                    poem.Author = Console.ReadLine();
                    break;
                case "3":
                    Console.Write("Новий рік: ");
                    poem.Year = int.Parse(Console.ReadLine());
                    break;
                case "4":
                    Console.Write("Нова тема: ");
                    poem.Theme = Console.ReadLine();
                    break;
                case "5":
                    Console.WriteLine("Новий текст (для завершення введіть порожній рядок):");
                    string text = "";
                    while (true)
                    {
                        string line = Console.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            break;
                        text += line + "\n";
                    }
                    poem.Text = text.TrimEnd('\n');
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    return;
            }

            Console.WriteLine("Вірш оновлено!");
        }

        public void SearchPoems()
        {
            Console.WriteLine("\nВиберіть параметр пошуку:");
            Console.WriteLine("1. За назвою");
            Console.WriteLine("2. За автором");
            Console.WriteLine("3. За темою");
            Console.WriteLine("4. За словом у тексті");
            Console.WriteLine("5. За роком");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();
            List<Poem> results = new List<Poem>();

            switch (choice)
            {
                case "1":
                    Console.Write("Введіть назву: ");
                    string title = Console.ReadLine();
                    results = poems.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "2":
                    Console.Write("Введіть автора: ");
                    string author = Console.ReadLine();
                    results = poems.Where(p => p.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "3":
                    Console.Write("Введіть тему: ");
                    string theme = Console.ReadLine();
                    results = poems.Where(p => p.Theme.Contains(theme, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "4":
                    Console.Write("Введіть слово: ");
                    string word = Console.ReadLine();
                    results = poems.Where(p => p.Text.Contains(word, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "5":
                    Console.Write("Введіть рік: ");
                    int year = int.Parse(Console.ReadLine());
                    results = poems.Where(p => p.Year == year).ToList();
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    return;
            }

            if (results.Count == 0)
            {
                Console.WriteLine("Нічого не знайдено!");
            }
            else
            {
                Console.WriteLine($"\nЗнайдено віршів: {results.Count}");
                foreach (var poem in results)
                {
                    poem.Display();
                }
            }
        }

        public void SaveToFile()
        {
            Console.Write("Введіть назву файлу для збереження: ");
            string filename = Console.ReadLine();

            try
            {
                string json = JsonSerializer.Serialize(poems, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, json);
                Console.WriteLine("Колекцію збережено успішно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            Console.Write("Введіть назву файлу для завантаження: ");
            string filename = Console.ReadLine();

            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine("Файл не знайдено!");
                    return;
                }

                string json = File.ReadAllText(filename);
                poems = JsonSerializer.Deserialize<List<Poem>>(json);
                Console.WriteLine($"Завантажено віршів: {poems.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження: {ex.Message}");
            }
        }

        public void GenerateReports()
        {
            Console.WriteLine("\nВиберіть тип звіту:");
            Console.WriteLine("1. За назвою вірша");
            Console.WriteLine("2. За ПІБ автора");
            Console.WriteLine("3. За темою вірша");
            Console.WriteLine("4. За словом у тексті");
            Console.WriteLine("5. За роком написання");
            Console.WriteLine("6. За довжиною вірша");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            Console.Write("Зберегти у файл? (y/n): ");
            bool saveToFile = Console.ReadLine().ToLower() == "y";

            string report = "";

            switch (choice)
            {
                case "1":
                    report = GenerateReportByTitle();
                    break;
                case "2":
                    report = GenerateReportByAuthor();
                    break;
                case "3":
                    report = GenerateReportByTheme();
                    break;
                case "4":
                    report = GenerateReportByWord();
                    break;
                case "5":
                    report = GenerateReportByYear();
                    break;
                case "6":
                    report = GenerateReportByLength();
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    return;
            }

            if (saveToFile)
            {
                Console.Write("Введіть назву файлу для звіту: ");
                string filename = Console.ReadLine();
                File.WriteAllText(filename, report);
                Console.WriteLine("Звіт збережено у файл!");
            }
            else
            {
                Console.WriteLine("\n" + report);
            }
        }

        private string GenerateReportByTitle()
        {
            var sorted = poems.OrderBy(p => p.Title).ToList();
            string report = "ЗВІТ ЗА НАЗВОЮ ВІРША\n";
            report += "=================================\n\n";

            foreach (var poem in sorted)
            {
                report += $"Назва: {poem.Title}\n";
                report += $"Автор: {poem.Author}\n";
                report += $"Рік: {poem.Year}\n";
                report += $"Тема: {poem.Theme}\n";
                report += "=================================\n";
            }

            return report;
        }

        private string GenerateReportByAuthor()
        {
            var sorted = poems.OrderBy(p => p.Author).ToList();
            string report = "ЗВІТ ЗА ПІБ АВТОРА\n";
            report += "=================================\n\n";

            foreach (var poem in sorted)
            {
                report += $"Автор: {poem.Author}\n";
                report += $"Назва: {poem.Title}\n";
                report += $"Рік: {poem.Year}\n";
                report += $"Тема: {poem.Theme}\n";
                report += "=================================\n";
            }

            return report;
        }

        private string GenerateReportByTheme()
        {
            var sorted = poems.OrderBy(p => p.Theme).ToList();
            string report = "ЗВІТ ЗА ТЕМОЮ ВІРША\n";
            report += "=================================\n\n";

            foreach (var poem in sorted)
            {
                report += $"Тема: {poem.Theme}\n";
                report += $"Назва: {poem.Title}\n";
                report += $"Автор: {poem.Author}\n";
                report += $"Рік: {poem.Year}\n";
                report += "=================================\n";
            }

            return report;
        }

        private string GenerateReportByWord()
        {
            Console.Write("Введіть слово для пошуку: ");
            string word = Console.ReadLine();

            var filtered = poems.Where(p => p.Text.Contains(word, StringComparison.OrdinalIgnoreCase)).ToList();
            string report = $"ЗВІТ ЗА СЛОВОМ У ТЕКСТІ: '{word}'\n";
            report += "=================================\n\n";

            foreach (var poem in filtered)
            {
                report += $"Назва: {poem.Title}\n";
                report += $"Автор: {poem.Author}\n";
                report += $"Рік: {poem.Year}\n";
                report += $"Тема: {poem.Theme}\n";
                report += "=================================\n";
            }

            return report;
        }

        private string GenerateReportByYear()
        {
            var sorted = poems.OrderBy(p => p.Year).ToList();
            string report = "ЗВІТ ЗА РОКОМ НАПИСАННЯ\n";
            report += "=================================\n\n";

            foreach (var poem in sorted)
            {
                report += $"Рік: {poem.Year}\n";
                report += $"Назва: {poem.Title}\n";
                report += $"Автор: {poem.Author}\n";
                report += $"Тема: {poem.Theme}\n";
                report += "=================================\n";
            }

            return report;
        }

        private string GenerateReportByLength()
        {
            var sorted = poems.OrderBy(p => p.Length).ToList();
            string report = "ЗВІТ ЗА ДОВЖИНОЮ ВІРША\n";
            report += "=================================\n\n";

            foreach (var poem in sorted)
            {
                report += $"Довжина: {poem.Length} символів\n";
                report += $"Назва: {poem.Title}\n";
                report += $"Автор: {poem.Author}\n";
                report += $"Рік: {poem.Year}\n";
                report += $"Тема: {poem.Theme}\n";
                report += "=================================\n";
            }

            return report;
        }

        public void ShowAllPoems()
        {
            if (poems.Count == 0)
            {
                Console.WriteLine("Колекція порожня!");
                return;
            }

            Console.WriteLine($"\nВсього віршів: {poems.Count}\n");
            foreach (var poem in poems)
            {
                poem.Display();
            }
        }
    }
}
