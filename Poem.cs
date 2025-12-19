using System;

namespace PoemCollection
{
    public class Poem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }

        public int Length
        {
            get { return Text.Length; }
        }

        public void Display()
        {
            Console.WriteLine("=================================");
            Console.WriteLine($"Назва: {Title}");
            Console.WriteLine($"Автор: {Author}");
            Console.WriteLine($"Рік: {Year}");
            Console.WriteLine($"Тема: {Theme}");
            Console.WriteLine($"Текст:\n{Text}");
            Console.WriteLine($"Довжина: {Length} символів");
            Console.WriteLine("=================================");
        }
    }
}
