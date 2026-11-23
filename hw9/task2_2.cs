public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public class Player
{
    public string Name;
    public int HP;
    public int Damage;

    public bool IsDead => HP <= 0;

    public Player(string name, int hp, int damage)
    {
        Name = name;
        HP = hp;
        Damage = damage;
    }

    public void Heal(int amount)
    {
        HP += amount;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }
}

public class HealthPotion : IUsableItem
{
    public string Name => "зелье оз";
    public string Description => "восстанавливает 50 HP";
    public Rarity Rarity => Rarity.Common;

    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine("Вы выпили зелье и восстановили 50 HP.");
    }
}

public class Sword : IUsableItem
{
    public string Name => "меч";
    public string Description => "увеличивает урон на 10";
    public Rarity Rarity => Rarity.Rare;

    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine("Вы использовали меч. урон увеличен на 10.");
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "свиток";
    public string Description => "наносит 20 урона. увеличивает урон на 30";
    public Rarity Rarity => Rarity.Legendary;

    public void Use(Player player)
    {
        player.HP -= 20;
        if (player.HP < 0)
            player.HP = 0;

        player.IncreaseDamage(30);
        Console.WriteLine(
            "свиток нанёс вам 20 урона, но увеличил ваш урон на 30.");
    }
}

public class Program
{
    public static void Main()
    {
        Player player = new Player("Артур", 100, 15);

        List<IUsableItem> items = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };

        foreach (IUsableItem item in items)
        {
            Console.WriteLine($"Используется: {item.Name} ({item.Rarity})");
            item.Use(player);
        }

        Console.WriteLine($"Имя: {player.Name}");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");
        Console.WriteLine($"Состояние: {(player.IsDead ? "мертв" : "жив")}");
    }
}

