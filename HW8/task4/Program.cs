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
        Console.WriteLine(json);
        
        GameCharacter newChel = JsonSerializer.Deserialize<GameCharacter>(json);
        Console.WriteLine(newChel.Name);
        Console.WriteLine(newChel.Inventory.Length);


    }
}