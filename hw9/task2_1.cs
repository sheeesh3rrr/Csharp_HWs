public interface ICombatant
{
    public string Name { get; }
    public int HP { get; set; }
    public bool IsDead { get; }
    public int Damage { get; }
    public void Attack(ICombatant target);
    public void TakeDamage(int amount);
}

public class Knight : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 150;
    public bool IsDead => HP <= 0;
    public int Damage { get; } = 25;

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine("�� ������");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine("���� ������");
            return;
        }

        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP < 0)
        {
            HP = 0;
        }
           
        Console.WriteLine($"������ {Name} ������� {amount} �����. �������� HP: {HP}");
    }
    
    public Knight(string name)
    {
        Name = name;
    }
}

public class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 80;
    public bool IsDead => HP <= 0;
    public int Damage { get; } = 40;

    public void Attack(ICombatant target)
    {
        for (int i = 0; i < 2; i++)
        {
            if (IsDead)
            {
                Console.WriteLine("�� ������");
                return;
            }

            if (target.IsDead)
            {
                Console.WriteLine($"��� {Name} �� ����� ��������� ������ ����");
                return;
            }

            target.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP < 0)
        {
            HP = 0;
        }

        Console.WriteLine($"��� {Name} ������� {amount} �����. �������� HP: {HP}");
    }

    public Mage(string name)
    {
        Name = name;
    }
}

public claass Program
{
    public static void Main()
    {
        ICombatant knight = new Knight("�����");
        ICombatant mage = new Mage("������");

        while (!knight.IsDead && !mage.IsDead)
        {
            Console.WriteLine($"������ {knight.Name} �������!");
            knight.Attack(mage);

            Console.WriteLine($"��� {mage.Name} �������!");
            mage.Attack(knight);
        }

        if (knight.IsDead)
        {
            Console.WriteLine($"������� ��� {mage.Name}!");
        }
        else
        {
            Console.WriteLine($"������� ������ {knight.Name}!");
        }
    }
}