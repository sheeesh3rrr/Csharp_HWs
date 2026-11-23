class Program
{
    static void Main()
    {
        Player player = new Player("Чувак", 100, 5);
        List<IUsableItem> items = new List<IUsableItem>()
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll()
        };
        foreach (var item in items)
        {
            Console.WriteLine($"Используем: {item.Name}");
            item.Use(player);
        }
        
        Console.WriteLine($"Имя: {player.Name}");
        Console.WriteLine($"Здоровье: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");

        if (player.HP > 0)
        {
            Console.WriteLine("Игрок жив");
        }
        else
        {
            Console.WriteLine("Игрок мертв");
        }
    }
}