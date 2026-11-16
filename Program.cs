using System.Diagnostics;
using System.Text.Json;

public class GameCharacter
{
    public string Name { get; set; }
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; }
}
public class GameSettings()
{
    public string PlayerName { get; set; }
    public int Difficulty { get; set; }
    public int MusicVulume { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 4 ===");
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        var hero = new GameCharacter
        {
            Name = "Bob",
            Level = 13,
            Health = 100.0,
            Inventory = new string[] { "Wooden sword", "Something unusual", "Bobby" }
        };

        string json = JsonSerializer.Serialize(hero, options);
        Console.WriteLine(json);

        GameCharacter backUp = JsonSerializer.Deserialize<GameCharacter>(json);
        Console.WriteLine($"Character name: {backUp.Name}, Things in inventory: {backUp.Inventory.Length}");

        Console.WriteLine("\n=== 5 ===");
        string path = Path.Combine(AppContext.BaseDirectory, "save.json");
        File.WriteAllText(path, json);

        string jsonRead = File.ReadAllText(path);
        GameCharacter loaded = JsonSerializer.Deserialize<GameCharacter>(jsonRead);
        Console.WriteLine("=== Loaded character ===");
        Console.WriteLine($"Name: {loaded.Name}");
        Console.WriteLine($"Level: {loaded.Level}");
        Console.WriteLine($"Health: {loaded.Health}");
        Console.WriteLine($"Inventory: {string.Join(", ", loaded.Inventory)}");

        Console.WriteLine("\n=== 6 ===");
        string path2 = Path.Combine(AppContext.BaseDirectory, "settings.json");
        var options2 = new JsonSerializerOptions { WriteIndented = true, };

        if (File.Exists(path2))
        {
            string json2 = File.ReadAllText(path2);
            GameSettings settings = JsonSerializer.Deserialize<GameSettings>(json2);
            Console.WriteLine("=== Current settings ===");
            Console.WriteLine($"Player name: {settings.PlayerName}");
            Console.WriteLine($"Defficulty: {settings.Difficulty}");
            Console.WriteLine($"Music volume: {settings.MusicVulume}");
        }
        else
        {
            Console.WriteLine("Settings file not found. Creating new one");
            Console.Write("Enter player name: ");
            string name = Console.ReadLine();

            Console.Write("Enter difficulty (1 - easy, 2 - mid, 3 - hard): ");
            int difficulty = int.Parse(Console.ReadLine());

            Console.Write("Enter music volume (0-100): ");
            int volume = int.Parse(Console.ReadLine());

            var settings = new GameSettings
            {
                PlayerName = name,
                Difficulty = difficulty,
                MusicVulume = volume
            };

            File.WriteAllText(path,JsonSerializer.Serialize(settings,options));
            Console.WriteLine("Successful");
        }

    }
}