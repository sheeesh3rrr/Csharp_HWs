
using System;
using System.Collections.Generic;

namespace ConsoleApp4.Task2._2
{
    public static class ItemsProgram
    {
        public static void Run()
        {
            var player = new Player("Игрок");

            List<IUsableItem> items = new List<IUsableItem>
            {
                new HealthPotion(),
                new Sword(),
                new AncientScroll()
            };

            foreach (IUsableItem item in items)
            {
                Console.WriteLine(
                    $"\nИспользуем: {item.Name} ({item.Rarity}) — {item.Description}");

                item.Use(player);
            }

            Console.WriteLine("\nИтоговые характеристики:");
            Console.WriteLine($"Имя: {player.Name}");
            Console.WriteLine($"HP: {player.HP}");
            Console.WriteLine($"Урон: {player.Damage}");
            Console.WriteLine($"Состояние: {(player.HP > 0 ? "жив" : "мёртв")}");
        }
    }
}
