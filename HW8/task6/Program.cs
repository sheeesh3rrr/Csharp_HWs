using System;
using System.Text.Json;

class Program
{
    static void Main()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameSettings settings = JsonSerializer.Deserialize<GameSettings>(json);
            
            Console.WriteLine($"Имя: {settings.PlayerName}");
            Console.WriteLine($"Сложность: {settings.Difficulty}");
            Console.WriteLine($"Громкость музыки: {settings.MusicVolume}");
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");
            
            GameSettings settings = new GameSettings();
            
            Console.WriteLine("Введите ваше имя:");
            settings.PlayerName = Console.ReadLine();
            
            Console.WriteLine("Выберите сложность (1 — легко, 2 — нормально, 3 — сложно):");
            settings.Difficulty = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Введите громкость музыки:");
            settings.MusicVolume = int.Parse(Console.ReadLine());
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(path, json);
            
            Console.WriteLine("Настройки сохранены!");
        }
    }
}