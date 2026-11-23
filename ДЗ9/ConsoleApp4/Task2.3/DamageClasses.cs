
using System;

namespace ConsoleApp4.Task2._3
{
    public interface IDamageCalculator
    {
        int CalculateDamage(int baseDamage, int targetArmor);
    }

    public class PhysicalDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            return Math.Max(1, baseDamage - targetArmor / 2);
        }
    }

    public class MagicDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int damage = targetArmor == 0
                ? baseDamage * 2
                : baseDamage - targetArmor / 4;

            return Math.Max(1, damage);
        }
    }

    public class FireDamageCalculator : IDamageCalculator
    {
        public int CalculateDamage(int baseDamage, int targetArmor)
        {
            int bonus = baseDamage > 50 ? 10 : 0;
            return Math.Max(1, baseDamage - targetArmor / 10 + bonus);
        }
    }

    public class Unit
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Armor { get; }

        public Unit(string name, int hp, int armor)
        {
            Name = name;
            HP = Math.Max(0, hp);
            Armor = Math.Max(0, armor);
        }
    }

    public class DamageSystem
    {
        private readonly IDamageCalculator _calculator;

        public DamageSystem(IDamageCalculator calculator)
        {
            _calculator = calculator;
        }

        public void ApplyDamage(Unit target, int baseDamage)
        {
            int damage = _calculator.CalculateDamage(baseDamage, target.Armor);
            target.HP = Math.Max(0, target.HP - damage);

            Console.WriteLine(
                $"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
        }
    }
    // Чтобы добавить ледяной урон, достаточно создать IceDamageCalculator,
    // реализовать в нём IDamageCalculator и передать его в конструктор DamageSystem.
    // Сам класс DamageSystem при этом менять не требуется: это принцип открытости/закрытости
    // и инверсия зависимости через интерфейс.

}
