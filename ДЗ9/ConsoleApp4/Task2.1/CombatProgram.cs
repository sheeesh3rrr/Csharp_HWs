using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Task2._1
{
    public static class CombatProgram
    {
        public static void Run()
        {
            ICombatant knight = new Knight("Артур");
            ICombatant mage = new Mage("Мерлин");

            while (!knight.IsDead && !mage.IsDead)
            {
                knight.Attack(mage);

                if (!mage.IsDead)
                {
                    mage.Attack(knight);
                }
            }

            Console.WriteLine(mage.IsDead
                ? $"Победил Рыцарь {knight.Name}!"
                : $"Победил Маг {mage.Name}!");
        }
    }
}
