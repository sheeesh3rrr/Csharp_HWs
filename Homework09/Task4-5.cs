using System;
using System.Text.Json;

public class GameCharacter
{
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; } = Array.Empty<string>();
}

public static class Program
{
    public static void Main()
    {

        // 4
        GameCharacter character = new GameCharacter
        {
            Name = "Biba",
            Level = 1,
            Health = 99.8,
            Inventory = new string[] { "Banana", "Boba", "Pistolet" }
        };

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(character, options);

        Console.WriteLine("JSON");
        Console.WriteLine(json);

        GameCharacter? newCharacter = JsonSerializer.Deserialize<GameCharacter>(json);

        Console.WriteLine("newCharacter");
        Console.WriteLine(newCharacter.Name);
        //Console.WriteLine(newCharacter.Health);
        Console.WriteLine(newCharacter.Inventory.Length);


        // 5
        string path = Path.Combine(
            AppContext.BaseDirectory, "save.json"
        );

        File.WriteAllText(path, json);

        string readjson = File.ReadAllText(path);

        GameCharacter? readCharacter = JsonSerializer.Deserialize<GameCharacter>(readjson);

        Console.WriteLine("=== Загруженный персонаж ===");
        Console.WriteLine($"Имя: {readCharacter.Name}");
        Console.WriteLine($"Уровень: {readCharacter.Level}");
        Console.WriteLine($"Здоровье: {readCharacter.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(", ", readCharacter.Inventory)}");  


    }
}