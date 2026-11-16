# HW9(15.11)
# Задача 1
а) Попадут свойства Name и Score, потому что публичные свойства. Поля secretCode и IsAlive не попадут, так как по умолчанию поля не включаются в Json-строку независимо от модификатора доступа.

б)В классе перед тем как объявить поле secretCode нужно написать атрибут [JsonInclude]

в)Можно также перед объявлением поля IsAlive написать атрибут  [JsonInclude], а можно переопределить JsonSerializerOptions IncludeFields = true
# Задача 2
а)
public class T
    {
        public string Title {get; set;}
        public int Year {get; set;}
        public double Rating {get; set;}
        public string[] Genres { get; set; }
        public bool IsMultiplayer { get; set; }
    }

б)
Вывелся null потому что по умолчанию JSON  чувствителен к регистру, ключ к нём не соответствует названию свойства, поэтому сериализатор не может его найти и выдаёт null. Чтобы JSON не обращал внимание на регистр ключей можно переопределять JsonSerializerOptions:
```
JsonSerializerOptions = new()
{
	PropertyNameCaseInsensitive = true
};
```
в) Является валидным JSON, её можно десериализировать в тип object[] 

# Задача 3
1. File — с небольшими по объёму файлами для простых разовых операций лучше использовать File
2. File — небольшая разовая операция - лучше использовать File, он автоматизирует частые операции. Не нужно вручную следить за тем, чтобы поток закрылся (как в случае с FileStream)
3. FileStream — лучше использовать FileStream, потому что это работа с большим файлом и лучше иметь возможность тонкого управлениея чтением и записью. Записывать данные в конце файла можно без перезаписывания предыдущих 
4. File — лучше использовать File для проверки наличия файла и в том случае, когда файл нужно записать целиком
5. FileStream — работа с большим файлам, обратиться к середине файла можно Благодаря Seek и Position

# Задача 4
## Main
```
using System;
using System.Text.Json;
namespace homework;
class Program
{
    static void Main(string[] args)
    {
        GameCharacter player = new GameCharacter("Steve", 0, 100, new string[] {"Sword", "Book" , "Bow" });

        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(player, options);
        Console.WriteLine(json);

        GameCharacter loadedPlayer = JsonSerializer.Deserialize<GameCharacter>(json, options);
        Console.WriteLine(loadedPlayer.Name);
        Console.WriteLine(loadedPlayer.Inventory.Length);
    }   
}
```
## Class GameCharacter
```
using System;
namespace homework
{
 public class GameCharacter
    {
  public string Name { get; set; }
  public int Level { get; set; }
  public double Health { get; set; }
  public string[] Inventory { get; set; }

  public GameCharacter() { }

  public GameCharacter(string name, int level, double health, string[] startItems)
  {
   Name = name;
   Level = level;
   Health = health;
   Inventory = startItems;
  }
 }
}
```
# Задача 5
## Main
```
using System;
using System.Text.Json;
namespace homework;
class Program
{
    static void Main(string[] args)
    {
        GameCharacter player = new GameCharacter("Steve", 0, 100, new string[] { "Sword", "Book", "Bow" });

        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(player, options);

        Console.WriteLine("Нажмите, чтобы записать файл");
        Console.ReadLine();

        string path = Path.Combine(AppContext.BaseDirectory, "save.json");
        File.WriteAllText(path, json);

        Console.WriteLine("Нажмите, чтобы прочитать файл");
        Console.ReadLine();

        string savedJson = File.ReadAllText(path);
        GameCharacter loadedPlayer = JsonSerializer.Deserialize<GameCharacter>(savedJson, options)!;

        Console.WriteLine("=== Загруженный персонаж ===");
        Console.WriteLine($"Имя: {loadedPlayer.Name}");
        Console.WriteLine($"Уровень: {loadedPlayer.Level}");
        Console.WriteLine($"Здоровье: {loadedPlayer.Health}");
        Console.WriteLine($"Инвентарь: {string.Join(", ", loadedPlayer.Inventory)}");
    }
}
```
## Class GameCharacter
```
using System;
namespace homework
{
 public class GameCharacter
    {
  public string Name { get; set; }
  public int Level { get; set; }
  public double Health { get; set; }
  public string[] Inventory { get; set; }

  public GameCharacter() { }

  public GameCharacter(string name, int level, double health, string[] startItems)
  {
   Name = name;
   Level = level;
   Health = health;
   Inventory = startItems;
  }
 }
}
```
# Задача 6
## Main
```
using System;
using System.Text.Json;
namespace homework;
class Program
{

    static void Main(string[] args)
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        GameSettings settings;

        string path = Path.Combine(AppContext.BaseDirectory, "settings.json");
        if (File.Exists(path))
        {
            string savedJson = File.ReadAllText(path);
            settings = JsonSerializer.Deserialize<GameSettings>(savedJson, options)!;
            Console.WriteLine("=== Настройки игры ===");
            Console.WriteLine($"Имя игрока: {settings.PlayerName}");
            Console.WriteLine($"Сложность: {settings.Difficulty}");
            Console.WriteLine($"Громкость: {settings.MusicVolume}");
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");
            settings = new GameSettings();
            Console.Write("Введите имя: ");
            settings.PlayerName = Console.ReadLine();
            Console.Write("Введите сложность (1 — легко, 2 — нормально, 3 — сложно): ");

            string inputDiff = Console.ReadLine();
            settings.Difficulty = (int.TryParse(inputDiff, out int diff) && 1 <= diff && diff <= 3? diff  : 1);

            Console.Write("Введите громкость музыки (0–100): ");
            string inputmusicVolume = Console.ReadLine();
            settings.MusicVolume = (int.TryParse(inputmusicVolume, out int m) && 0 <= m && m <= 100 ? m : 50);

            Console.WriteLine("Нажмите для сохранения файла: ");
            Console.ReadLine();

            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(path, json);
        }    
    }
}
```
## Class GameSettings
```
using System;
namespace homework
{
	public class GameSettings
    {
		public string PlayerName { get; set; }
		public int Difficulty { get; set; }
		public int MusicVolume { get; set; }
	}
}
```



