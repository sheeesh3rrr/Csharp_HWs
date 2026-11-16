//using System.Text;
//using System.Text.Json;

//public class GameCharacter
//{
//    public string Name { get; set; } = string.Empty;
//    public int Level { get; set; }
//    public double Health { get; set; }
//    public string[] Inventory { get; set; }

//}

//class Program
//{
//    static void Main()
//    {
//        GameCharacter Bob = new GameCharacter
//        {
//            Name = "Bob",
//            Level = 5,
//            Health = 67,
//            Inventory = new string[] { "bottle of milk", "love potion", "stick", "helmet" }
//        };

//        var options = new JsonSerializerOptions
//        {
//            WriteIndented = true, /* Включаю добавление отступов */
//        };

//        string jsonString = JsonSerializer.Serialize(Bob, options);

//        Console.WriteLine("--- Сериализованный JSON ---");
//        Console.WriteLine(jsonString);
//        Console.WriteLine();

//        GameCharacter? DeserializedBob = JsonSerializer.Deserialize<GameCharacter>(jsonString);
        
//        if (DeserializedBob != null) /* Проверка, что в целом  хоть что-то есть */
//        {
//            Console.WriteLine("--- Персонаж после десериализации ---");
//            Console.WriteLine($"Имя персонажа: {DeserializedBob.Name}");
//            Console.WriteLine($"Количество предметов в инвентаре: {DeserializedBob.Inventory.Length}");
//        }
//    }
//}