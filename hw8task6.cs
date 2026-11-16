
using System;
using System.IO;
using System.Text.Json;

public class GameSettings
{
    public string PlayerName { get; set; } = "";
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }
}

class Program
{
    static void Main()
    {
        string path = Path.Combine(
            AppContext.BaseDirectory,
            "settings.json"
        );

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        GameSettings settings;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            settings = JsonSerializer.Deserialize<GameSettings>(json)!;

            Console.WriteLine("=== Текущие настройки ===");
            Console.WriteLine($"Имя игрока: {settings.PlayerName}");
            Console.WriteLine($"Сложность: {settings.Difficulty}");
            Console.WriteLine($"Громкость музыки: {settings.MusicVolume}");
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");

            settings = new GameSettings();

            Console.Write("Введите имя игрока: ");
            settings.PlayerName = Console.ReadLine() ?? "";

            Console.Write("Введите сложность (1 - легко, 2 - нормально, 3 - сложно): ");
            settings.Difficulty = int.Parse(Console.ReadLine()!);

            Console.Write("Введите громкость музыки (0-100): ");
            settings.MusicVolume = int.Parse(Console.ReadLine()!);

            string json = JsonSerializer.Serialize(settings, options);

            File.WriteAllText(path, json);

            Console.WriteLine("Настройки сохранены!");
        }
    }
}
