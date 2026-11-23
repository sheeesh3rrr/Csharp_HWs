
using System;

namespace ConsoleApp4.Task2._3
{
    public static class DamageProgram
    {
        public static void Run()
        {
            var target = new Unit("Голем", 200, 20);
            const int baseDamage = 60;

            Console.WriteLine("Физический урон:");
            new DamageSystem(new PhysicalDamageCalculator())
                .ApplyDamage(target, baseDamage);

            target.HP = 200;

            Console.WriteLine("\nМагический урон:");
            new DamageSystem(new MagicDamageCalculator())
                .ApplyDamage(target, baseDamage);

            target.HP = 200;

            Console.WriteLine("\nОгненный урон:");
            new DamageSystem(new FireDamageCalculator())
                .ApplyDamage(target, baseDamage);
        }
    }
}
