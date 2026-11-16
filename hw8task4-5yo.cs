
using System;
using System.Text.Json;
using System.IO;
public class GameCharacter
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
}

class Program
{
    static void Main()
    {
        GameCharacter character = new GameCharacter
        {
            Name = "Любитель сишарпа",
            Level = 67,
            Health = 52.9,
            Inventory = new string[]
            {
                "Мучения",
                "Классы",
                "Сигареты"
            }
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(character, options);

        //Console.WriteLine("=== JSON ===");
        //Console.WriteLine(json);

        //GameCharacter? newCharacter =
        //    JsonSerializer.Deserialize<GameCharacter>(json);

        //Console.WriteLine("\n=== Восстановленный персонаж ===");
        //Console.WriteLine($"Имя: {newCharacter!.Name}");
        //Console.WriteLine($"Количество предметов: {newCharacter.Inventory.Length}");
        
        string path = Path.Combine(
        AppContext.BaseDirectory,
        "save.json"
        );

        File.WriteAllText(path, json);

        string loadedJson = File.ReadAllText(path);

        GameCharacter? loadedCharacter =
            JsonSerializer.Deserialize<GameCharacter>(loadedJson);

        Console.WriteLine("=== Загруженный персонаж ===");
        Console.WriteLine($"Имя: {loadedCharacter!.Name}");
        Console.WriteLine($"Уровень: {loadedCharacter.Level}");
        Console.WriteLine($"Здоровье: {loadedCharacter.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(", ", loadedCharacter.Inventory)}");
    }
}
