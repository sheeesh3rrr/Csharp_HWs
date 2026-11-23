
using System;

namespace ConsoleApp4.Task2._2
{
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
        public string Name { get; }
        public int HP { get; private set; }
        public int Damage { get; private set; }

        public Player(string name, int hp = 100, int damage = 10)
        {
            Name = name;
            HP = Math.Max(0, hp);
            Damage = Math.Max(0, damage);
        }

        public void Heal(int amount)
        {
            HP += Math.Max(0, amount);
        }

        public void TakeDamage(int amount)
        {
            HP = Math.Max(0, HP - Math.Max(0, amount));
        }

        public void IncreaseDamage(int amount)
        {
            Damage += Math.Max(0, amount);
        }
    }

    public class HealthPotion : IUsableItem
    {
        public string Name => "Зелье здоровья";
        public string Description => "Восстанавливает 50 HP.";
        public Rarity Rarity => Rarity.Common;

        public void Use(Player player)
        {
            player.Heal(50);
            Console.WriteLine("Вы выпили зелье и восстановили 50 HP.");
        }
    }

    public class Sword : IUsableItem
    {
        public string Name => "Меч";
        public string Description => "Увеличивает урон на 10.";
        public Rarity Rarity => Rarity.Rare;

        public void Use(Player player)
        {
            player.IncreaseDamage(10);
            Console.WriteLine("Вы взяли меч. Ваш урон увеличился на 10.");
        }
    }

    public class AncientScroll : IUsableItem
    {
        public string Name => "Древний свиток";
        public string Description => "Наносит 20 урона и увеличивает урон на 30.";
        public Rarity Rarity => Rarity.Legendary;

        public void Use(Player player)
        {
            player.TakeDamage(20);
            player.IncreaseDamage(30);
            Console.WriteLine("Проклятие свитка нанесло 20 урона, но ваша сила выросла на 30.");
        }
    }
}
