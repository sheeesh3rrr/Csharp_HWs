public class Player
{
    public int HP { get; set; }
    public int Damage { get; set; }
    public string Name { get; }
    
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
    public string Name => "Зелье здоровья";
    public string Description => "Восстанавливает 50 HP";
    public Rarity Rarity => Rarity.Common;
    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine("Вы выпили зелье и восстановили 50 HP!");
    }
}

public class Sword : IUsableItem
{
    public string Name => "Меч";
    public string Description => "Увеличивыет урон на 10";
    public Rarity Rarity => Rarity.Rare;
    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine("Урон увеличен на 10!");
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "Древний свиток";
    public string Description => "Проклятие наносит 20 урона, взамен увеличивая урон на 30";
    public Rarity Rarity => Rarity.Legendary;
    public void Use(Player player)
    {
        player.HP -= 20;
        player.IncreaseDamage(30);
        Console.WriteLine("Вы использовали древний свиток! -20 HP, +30 урона");
    }
}