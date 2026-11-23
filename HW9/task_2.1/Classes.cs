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
            Console.WriteLine($"Рыцарь {Name} мертв!");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"{target.Name} мертв!");
            return;
        }
        
        Console.WriteLine($"Рыцарь {Name} атакует {target.Name}!");
        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

public class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 80;
    public bool IsDead => HP <= 0;
    public int Damage { get; } = 40;
    
    public Mage(string name)
    {
        Name = name;
    }
    
    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Маг {Name} мертв!");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"Маг {Name} не может атаковать мёртвую цель");
            return;
        }
        
        Console.WriteLine($"Маг {Name} атакует {target.Name}!");
        target.TakeDamage(Damage);
        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}