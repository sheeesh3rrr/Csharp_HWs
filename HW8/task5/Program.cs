using System;
using System.Text.Json;

class Program
{
    static void Main()
    {
        GameCharacter chel = new GameCharacter()
        {
            Name = "Chel",
            Level = 1,
            Health = 100,
            Inventory = new string[] { "Sword", "Shield", "Potion" }
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        string json = JsonSerializer.Serialize(chel, options);
        string path = Path.Combine(Directory.GetCurrentDirectory(), "save.json");
        File.WriteAllText(path, json);
        
        string loadJson = File.ReadAllText(path);
        GameCharacter newChel = JsonSerializer.Deserialize<GameCharacter>(loadJson);
        
        Console.WriteLine("=== Загруженный персонаж ===");
        Console.WriteLine($"Имя: {newChel.Name}");
        Console.WriteLine($"Уровень: {newChel.Level}");
        Console.WriteLine($"Здоровье: {newChel.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(", ", newChel.Inventory)}");
        


    }
}