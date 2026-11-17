/*
Задание 1

а) Name и Score попадут так как это публичные свойства, secretCode не попадет потому что он приватный, а IsAlive не попадет потому что поля по умолчанию не сериализуются
б) добавить [JsonInclude] перед полем
в) 1 вариант - добавить [JsonInclude] как перед приватным полем, а второй вариант - создать объект JsonSerializerOptions с параметром IncludeFields = true и использовать его при сериализации



Задание 2
a) 
public class GameEntry{
    public string Title { get; set; }
    public int Year { get; set; }
    public float Rating { get; set; }
    public string[] Genres { get; set; }
    public bool IsMultiplayer { get; set; }
}

б) Регистр полей по умолчанию учитывается (программа не может найти поле с маленькой буквы), но мы можем это исправить, добавив в настройки PropertyNameCaseInsensitive = true

в) можно использовать List<Object> или Object[], так как от Object наследуются все типы



Задача 3

1) File. Нет смысла усложнять если нам просто нужно вывести содержимое
2) Если мы сохраняем ВСЕ данные игрока (то есть перезаписываем весь файл) то проще это сделать через File
3) Filestream, чтобы каждый раз не загружать весь тяжелый файл, а просто дописывать нужное в конец
4) Читаем целиком, значит File
5) Изменяем конкретное место и не загружаем целиком, значит Filestream



Задача 4

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

public class GameCharacter
{
    public string Name { get; set; }
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; }
    public GameCharacter()
    {
        Name = "A";
    }
}
public class Program
{
    static void Main(string[] args)
    {
        GameCharacter MC = new GameCharacter();
        MC.Name = "Main Character";
        MC.Level = 1;
        MC.Health = 100;
        string[] inv = {"Sword", "Spear", "Shield"};
        MC.Inventory = inv;
        
        JsonSerializerOptions options = new JsonSerializerOptions
        {
          WriteIndented = true,
          TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };

        string json = JsonSerializer.Serialize(MC, options);

        Console.WriteLine(json);

        GameCharacter CM = JsonSerializer.Deserialize<GameCharacter>(json, options);
        Console.WriteLine(CM.Name);
        Console.WriteLine(CM.Inventory.Length);
    }
}



Задача 5

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

public class GameCharacter
{
    public string Name { get; set; }
    public int Level { get; set; }
    public double Health { get; set; }
    public string[] Inventory { get; set; }
    public GameCharacter()
    {
        Name = "A";
    }
}
public class Program
{
    static void Main(string[] args)
    {
        GameCharacter MC = new GameCharacter();
        MC.Name = "Main Character";
        MC.Level = 1;
        MC.Health = 100;
        string[] inv = {"Sword", "Spear", "Shield"};
        MC.Inventory = inv;
        
        JsonSerializerOptions options = new JsonSerializerOptions
        {
          WriteIndented = true,
          TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };

        string json = JsonSerializer.Serialize(MC, options);

        string path = Path.Combine(AppContext.BaseDirectory, "save.json");
        File.WriteAllText(path, json);

        string read = File.ReadAllText(path);

        GameCharacter CM = JsonSerializer.Deserialize<GameCharacter>(read, options);
        Console.WriteLine($"=== Загруженный персонаж ===\nИмя: {CM.Name}\nУровень: {CM.Level}\nЗдоровье: {CM.Health}\nИнвентарь: {CM.Inventory[0]}, {CM.Inventory[1]}, {CM.Inventory[2]}");
    }
}



Задача 6
*/
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

public class GameSettings
{
    public string PlayerName { get; set; }
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }
}
public class Program
{
    static void Main(string[] args)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "settings.json");

        JsonSerializerOptions options = new JsonSerializerOptions
        {
          WriteIndented = true,
          TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };

        if (File.Exists(path))
        {
            string read = File.ReadAllText(path);

            GameSettings settings = JsonSerializer.Deserialize<GameSettings>(read, options);
            Console.WriteLine($"Настройки: \n Имя: {settings.PlayerName}, Сложность: {settings.Difficulty}, Громкость музыки: {settings.MusicVolume}");
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки. Введите имя, сложность и громкость музыки в отдельных строках");
            GameSettings settings = new GameSettings();
            settings.PlayerName = Console.ReadLine();
            settings.Difficulty = int.Parse(Console.ReadLine());
            settings.MusicVolume = int.Parse(Console.ReadLine());

            string json = JsonSerializer.Serialize(settings, options);

            File.WriteAllText(path, json);
        }

        
        


    }
}

