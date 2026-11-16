using System.Text.Json;

static class Program
{
    static void Main()
    {
        string csvPath = Path.Combine(AppContext.BaseDirectory, "monsters.csv");
        string jsonPath = Path.Combine(AppContext.BaseDirectory, "monsters.json");
        string[] lines = File.ReadAllLines(csvPath);
        List<Monster> monsters = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(';');
            monsters.Add(new Monster
            {
                Name = fields[0],
                Health = int.Parse(fields[1]),
                Damage = int.Parse(fields[2]),
                Type = fields[3]
            });
        }

        File.WriteAllText(jsonPath, JsonSerializer.Serialize(monsters));
        monsters = JsonSerializer.Deserialize<List<Monster>>(File.ReadAllText(jsonPath));

        foreach (Monster monster in monsters)
            Console.WriteLine($"[{monster.Type}] {monster.Name} HP: {monster.Health}, DMG: {monster.Damage}");

        Console.WriteLine($"Общее количество монстров: {monsters.Count}");
        if (monsters.Count == 0)
            return;

        Monster mostHealth = monsters[0];
        Monster mostDamage = monsters[0];
        foreach (Monster monster in monsters)
        {
            if (monster.Health > mostHealth.Health)
                mostHealth = monster;
            if (monster.Damage > mostDamage.Damage)
                mostDamage = monster;
        }

        Console.WriteLine($"Наибольшее здоровье: {mostHealth.Name} HP: {mostHealth.Health}");
        Console.WriteLine($"Наибольший урон: {mostDamage.Name} DMG: {mostDamage.Damage}");
    }
}
