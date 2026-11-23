class Program
{
    static void Main()
    {
        Unit unit = new Unit("Лошара", 200, 20);
        
        DamageSystem physical = new DamageSystem(new PhysicalDamageCalculator());
        DamageSystem magical = new DamageSystem(new MagicDamageCalculator());
        DamageSystem fire = new DamageSystem(new FireDamageCalculator());
        
        physical.ApplyDamage(unit, 50);
        magical.ApplyDamage(unit, 50);
        fire.ApplyDamage(unit, 50);
    }
}

// Чтобы добавить новый тип урона ледяной,
// нужно создать новый класс IceDamageCalculator,
// реализующий интерфейс IDamageCalculator.
// Затем передать его в конструктор DamageSystem.
// Сам класс DamageSystem изменять не нужно.