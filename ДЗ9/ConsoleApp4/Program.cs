using ConsoleApp4.Task2._1;
using ConsoleApp4.Task2._2;
using ConsoleApp4.Task2._3;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выбери задание:");
        Console.WriteLine("1 — Бой рыцаря и мага");
        Console.WriteLine("2 — Использование предметов");
        Console.WriteLine("3 — Система расчёта урона");

        string choice = Console.ReadLine();

        if (choice == "1")
            CombatProgram.Run();
        else if (choice == "2")
            ItemsProgram.Run();
        else if (choice == "3")
        {
            DamageProgram.Run();
        }
        else
            Console.WriteLine("Неверный выбор.");
    }
}