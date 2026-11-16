/*
 * №4 и №5
using System;
using System.IO;
using System.Text.Json;

public class GameCharacter
{
    public string Name { get; set; }
    public int Level {get; set; }
    public double Health {get; set; }
    public string[] Inventory {get; set; }
}

class Program
{
    static void Main()
    {
        GameCharacter character = new GameCharacter
        {   Name = "Артур",
            Level = 15,
            Health = 125.5,
            Inventory = new string[] { "Меч света", "Зелье лечения", "Щит дракона" }
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        
        string jsonString = JsonSerializer.Serialize(character, options);

        Console.WriteLine("=== Сериализованный JSON ===");
        Console.WriteLine(jsonString);

        string filePath = Path.Combine(AppContext.BaseDirectory, "save.json");

        File.WriteAllText(filePath, jsonString);

        string fileContent =File.ReadAllText(filePath);


        GameCharacter loadedCharacter = JsonSerializer.Deserialize<GameCharacter>(fileContent);
        Console.WriteLine("=== Загруженный персонаж ===");
        if (loadedCharacter != null)


        {
            Console.WriteLine($"Имя: {loadedCharacter.Name}");
            Console.WriteLine($"Уровень: {loadedCharacter.Level}");
            Console.WriteLine($"Здоровье: {loadedCharacter.Health}");
            Console.WriteLine($"Инвентарь: {string.Join(", ", loadedCharacter.Inventory)}");
        }
    }
}

*/
//№6
using System;
using System.IO;
using System.Text.Json;

public class GameSettings
{
    public string PlayerName { get; set; }
    public int Difficulty { get; set; }     // 1 — легко, 2 — нормально, 3 — сложно
    public int MusicVolume { get; set; }    // 0–100
}

class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "settings.json");
        GameSettings settings;

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            settings = JsonSerializer.Deserialize<GameSettings>(jsonString) ?? new GameSettings();

            Console.WriteLine("=== Текущие настройки игры (загружены из файла) ===");
            Console.WriteLine($"Имя игрока: {settings.PlayerName}");
            Console.WriteLine($"Сложность: {settings.Difficulty}");
            Console.WriteLine($"Громкость музыки: {settings.MusicVolume}%");
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаем новые настройки.");

            settings = new GameSettings();

            Console.Write("Введите имя игрока: ");
            settings.PlayerName = Console.ReadLine();

            Console.Write("Введите сложность (1 — легко, 2 — нормально, 3 — сложно): ");
            int.TryParse(Console.ReadLine(), out int difficulty);
            settings.Difficulty = difficulty;

            Console.Write("Введите громкость музыки (0–100): ");
            int.TryParse(Console.ReadLine(), out int volume);
            settings.MusicVolume = volume;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine("Настройки успешно сохранены!");
        }
    }
}