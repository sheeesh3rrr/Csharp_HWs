// Задача 1
// а) Будут только Name и Score, они свойства, а IsAlive и secretCode — поля, сериализатор обычно их пропускает
// б) Чтобы записать secretCode и оставить его private, надо поставить над ним [JsonInclude]
// в) Оставить IsAlive полем и поставить над ним [JsonInclude]
//    Включить сериализацию всех публичных полей через IncludeFields

// Задача 2
// а)
// public class Game
// {
//     public string Title { get; set; } = "";
//     public int Year { get; set; }
//     public double Rating { get; set; }
//     public string[] Genres { get; set; } = Array.Empty<string>();
//     public bool IsMultiplayer { get; set; }
// }
// б) По умолчанию JsonSerializer.Deserialize учитывает регистр имён, поэтому ключ title пропускается, а свойство Title сохраняет начальное значение, в классе выше это пустая строка, без инициализатора было бы null
// var options = new JsonSerializerOptions
// {
//     PropertyNameCaseInsensitive = true
// };
// Game? game = JsonSerializer.Deserialize<Game>(json, options);
// в) [1, "hello", true, null] — валидный JSON, в C# его можно десериализовать в JsonElement[]
// JsonElement[]? items =
//     JsonSerializer.Deserialize<JsonElement[]>("[1, \"hello\", true, null]");

// Задача 3
// Я не нашел где файл

using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        CharacterDemo(true);
        SettingsDemo();
    }

    static void CharacterDemo(bool useFile)
    {
// Задача 4
        var character = new GameCharacter
        {
            Name = "Артур",
            Level = 5,
            Health = 87.5,
            Inventory = new[] { "Меч", "Щит", "Зелье" }
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(character, options);
        Console.WriteLine(json);

        if (useFile)
        {
// Задача 5
string path = Path.Combine(AppContext.BaseDirectory, "save.json");
            File.WriteAllText(path, json);
            json = File.ReadAllText(path);
        }

        GameCharacter restored = JsonSerializer.Deserialize<GameCharacter>(json)!;
        if (useFile)
        {
            Console.WriteLine("=== Загруженный персонаж ===");
            Console.WriteLine($"Имя: {restored.Name}");
            Console.WriteLine($"Уровень: {restored.Level}");
            Console.WriteLine($"Здоровье: {restored.Health}");
            Console.WriteLine($"Инвентарь: {string.Join(", ", restored.Inventory)}");
        }
        else
        {
            Console.WriteLine($"Имя: {restored.Name}");
            Console.WriteLine($"Предметов в инвентаре: {restored.Inventory.Length}");
        }
    }

// Задача 6
static void SettingsDemo()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "settings.json");
        GameSettings? settings;
        if (File.Exists(path))
        {
            settings = JsonSerializer.Deserialize<GameSettings>(File.ReadAllText(path));
            if (settings is null)
            {
                Console.WriteLine("Не удалось загрузить настройки");
                return;
            }
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки");
            Console.Write("Имя игрока: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Введите имя игрока");
                return;
            }

            Console.Write("Сложность (1 — легко, 2 — нормально, 3 — сложно): ");
            if (!int.TryParse(Console.ReadLine(), out int difficulty) ||
                difficulty < 1 || difficulty > 3)
            {
                Console.WriteLine("Введите число от 1 до 3");
                return;
            }

            Console.Write("Громкость музыки (0–100): ");
            if (!int.TryParse(Console.ReadLine(), out int volume) ||
                volume < 0 || volume > 100)
            {
                Console.WriteLine("Введите число от 0 до 100");
                return;
            }

            settings = new GameSettings
            {
                PlayerName = name.Trim(),
                Difficulty = difficulty,
                MusicVolume = volume
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(path, JsonSerializer.Serialize(settings, options));
        }

        Console.WriteLine("=== Настройки игры ===");
        Console.WriteLine($"Имя игрока: {settings.PlayerName}");
        Console.WriteLine($"Сложность: {settings.Difficulty}");
        Console.WriteLine($"Громкость музыки: {settings.MusicVolume}%");
    }
}

public class Game
{
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; } = Array.Empty<string>();
    public bool IsMultiplayer { get; set; }
}

public class GameCharacter
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
}

public class GameSettings
{
    public string PlayerName { get; set; } = "";
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }
}