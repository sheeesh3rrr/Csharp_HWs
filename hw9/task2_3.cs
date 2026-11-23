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
        int damage = Math.Max(1, baseDamage - targetArmor / 4);

        if (targetArmor == 0)
            damage *= 2;

        return damage;
    }
}

public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = Math.Max(1, baseDamage - targetArmor / 10);

        if (baseDamage > 50)
            damage += 10;

        return damage;
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
        HP = hp;
        Armor = armor;
    }
}

public class DamageSystem
{
    private IDamageCalculator _calculator;

    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = _calculator.CalculateDamage(baseDamage, target.Armor);

        target.HP = Math.Max(0, target.HP - damage);

        Console.WriteLine($"юнит {target.Name} получил {damage} урона. осталось HP: {target.HP}");
    }
    
    public DamageSystem(IDamageCalculator calculator)
    {
        _calculator = calculator;
    }
}

public class Program
{
    public static void Main()
    {
        Unit knight = new Unit("гатс", 300, 20);

        DamageSystem physical = new DamageSystem(
            new PhysicalDamageCalculator());
        DamageSystem magic = new DamageSystem(
            new MagicDamageCalculator());
        DamageSystem fire = new DamageSystem(
            new FireDamageCalculator());

        physical.ApplyDamage(knight, 60);
        magic.ApplyDamage(knight, 60);
        fire.ApplyDamage(knight, 60);
    }
}