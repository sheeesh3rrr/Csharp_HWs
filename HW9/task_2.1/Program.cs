class Program
{
    static void Main()
    {
       Knight knight = new Knight("Артур");
       Mage mage = new Mage("Мерлин");
       
       while (!knight.IsDead && !mage.IsDead)
       {
           knight.Attack(mage);

           if (mage.IsDead) break;

           mage.Attack(knight);
       }

       if (knight.IsDead) Console.WriteLine($"Победил маг {mage.Name}!");
       else Console.WriteLine($"Победил рыцарь {knight.Name}!");
    }
}