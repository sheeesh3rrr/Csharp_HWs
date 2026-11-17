using System;
using System.IO;
using System.Text.Json;

public class GameCharacter
{
    public string Name { get; set; }
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; }
}

public class GameSettings
{
    public string PlayerName { get; set; }
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }
}

class Program
{
    static void Main()
    {
        // Задачи 4 и 5
        var character = new GameCharacter
        {
            Name = "Vika",
            Level = 1,
            Health = 100.0,
            Inventory = new string[] { "Кисть", "Планшет", "Стилус" }
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(character, options);

        string savePath = Path.Combine(AppContext.BaseDirectory, "save.json");
        File.WriteAllText(savePath, jsonString);

        string readJson = File.ReadAllText(savePath);
        GameCharacter loadedCharacter = JsonSerializer.Deserialize<GameCharacter>(readJson);

        Console.WriteLine("=== Загруженный персонаж ===");
        Console.WriteLine($"Имя: {loadedCharacter.Name}");
        Console.WriteLine($"Уровень: {loadedCharacter.Level}");
        Console.WriteLine($"Здоровье: {loadedCharacter.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(", ", loadedCharacter.Inventory)}");
        Console.WriteLine();

        // Задача 6
        string settingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        if (File.Exists(settingsPath))
        {
            string settingsJson = File.ReadAllText(settingsPath);
            GameSettings settings = JsonSerializer.Deserialize<GameSettings>(settingsJson);

            Console.WriteLine("Файл настроек найден. Текущие настройки:");
            Console.WriteLine($"Имя игрока: {settings.PlayerName}");
            Console.WriteLine($"Сложность: {settings.Difficulty}");
            Console.WriteLine($"Громкость музыки: {settings.MusicVolume}");
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");
            
            Console.Write("Введите имя игрока: ");
            string name = Console.ReadLine();
            
            Console.Write("Введите сложность (1 — легко, 2 — нормально, 3 — сложно): ");
            int difficulty = int.Parse(Console.ReadLine());
            
            Console.Write("Введите громкость музыки (0-100): ");
            int volume = int.Parse(Console.ReadLine());

            var newSettings = new GameSettings
            {
                PlayerName = name,
                Difficulty = difficulty,
                MusicVolume = volume
            };

            string newSettingsJson = JsonSerializer.Serialize(newSettings, options);
            File.WriteAllText(settingsPath, newSettingsJson);
            Console.WriteLine("Настройки успешно сохранены!");
        }
    }
}