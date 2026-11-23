using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Task2._1
{
    public class Knight : ICombatant
    {
        public string Name { get; }
        public int HP { get; set; } = 150;
        public bool IsDead => HP <= 0;
        public int Damage { get; } = 25;

        public Knight(string name)
        {
            Name = name;
        }

        public void Attack(ICombatant target)
        {
            if (IsDead)
            {
                Console.WriteLine($"Рыцарь {Name} не может атаковать: он мёртв.");
                return;
            }

            if (target.IsDead)
            {
                Console.WriteLine($"Рыцарь {Name} не может атаковать мёртвую цель.");
                return;
            }

            Console.WriteLine($"Рыцарь {Name} атакует!");
            target.TakeDamage(Damage);
        }

        public void TakeDamage(int amount)
        {
            HP = Math.Max(0, HP - Math.Max(0, amount));
            Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
        }
    }
}
