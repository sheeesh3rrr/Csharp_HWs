using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Monster
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public string Type { get; set; }
}

class Program
{
    static void Main()
    {
        string csvPath = Path.Combine(AppContext.BaseDirectory, "monsters.csv");
        string jsonPath = Path.Combine(AppContext.BaseDirectory, "monsters.json");

        if (!File.Exists(csvPath))
        {
            string[] defaultCsv = {
                "Name; Health; Damage; Type",
                "Goblin; 50; 10; Melee",
                "Dragon; 500; 80; Flying",
                "Skeleton; 30; 15; Melee",
                "Ghost; 40; 25; Magic",
                "Archer; 60; 35; Ranged"
            };
            File.WriteAllLines(csvPath, defaultCsv);
        }

        string[] lines = File.ReadAllLines(csvPath);
        List<Monster> monsters = new List<Monster>();

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] parts = lines[i].Split(';');
            if (parts.Length >= 4)
            {
                monsters.Add(new Monster
                {
                    Name = parts[0].Trim(),
                    Health = int.Parse(parts[1].Trim()),
                    Damage = int.Parse(parts[2].Trim()),
                    Type = parts[3].Trim()
                });
            }
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(monsters, options);
        File.WriteAllText(jsonPath, json);

        string readJson = File.ReadAllText(jsonPath);
        List<Monster> deserializedMonsters = JsonSerializer.Deserialize<List<Monster>>(readJson);

        Console.WriteLine("Список монстров из JSON");
        foreach (var m in deserializedMonsters)
        {
            Console.WriteLine($"[{m.Type}] {m.Name}");
            Console.WriteLine($"HP: {m.Health}, DMG: {m.Damage}\n");
        }

        if (deserializedMonsters.Count > 0)
        {
            int totalCount = deserializedMonsters.Count;
            Monster maxHealthMonster = deserializedMonsters[0];
            Monster maxDamageMonster = deserializedMonsters[0];

            foreach (var m in deserializedMonsters)
            {
                if (m.Health > maxHealthMonster.Health) maxHealthMonster = m;
                if (m.Damage > maxDamageMonster.Damage) maxDamageMonster = m;
            }

            Console.WriteLine("Статистика бестиария:");
            Console.WriteLine($"Общее количество монстров: {totalCount}");
            Console.WriteLine($"Монстр с наибольшим здоровьем: {maxHealthMonster.Name} (НР: {maxHealthMonster.Health})");
            Console.WriteLine($"Монстр с наибольшим уроном: {maxDamageMonster.Name} (DMG: {maxDamageMonster.Damage})");
        }
    }
}