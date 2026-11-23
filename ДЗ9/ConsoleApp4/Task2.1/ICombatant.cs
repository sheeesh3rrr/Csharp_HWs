using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Task2._1
{
     public interface ICombatant
    {
        string Name { get; }
        int HP { get; set; }
        bool IsDead { get; }
        int Damage { get; }

        void Attack(ICombatant target);
        void TakeDamage(int amount);
    }
}
