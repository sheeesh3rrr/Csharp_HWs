# Домашнее задание 8
## Задача 1. 

а) По умолчанию `System.Text.Json` сериализует публичные свойства класса. Поэтому в JSON попадут `Name` и `Score`.

Приватное поле `secretCode` не сериализуется, потому что оно является приватным.

Публичное поле `IsAlive` тоже не сериализуется по умолчанию, так как `System.Text.Json` не сериализует поля без дополнительных настроек.

б) Чтобы secretCode тоже попал в JSON можно использовать атрибут [JsonInclude]. Он позволяет включить поле в сериализацию, не меняя его модификатор доступа:
public class Player
{
    public string Name { get; set; }
    public int Score { get; set; }

    [JsonInclude]
    private int secretCode = 42;

    public bool IsAlive;
}

в)System.Text.Json по умолчанию сериализует публичные свойства, но не публичные поля. Поэтому IsAlive не попадёт в JSON. Есть два способа, чтобы решить проблему:
 1.Добавить атрибут [JsonInclude] перед полем:
[JsonInclude]
public bool IsAlive;

 2.Включить сериализацию всех публичных полей через настройки:
var options = new JsonSerializerOptions
{
    IncludeFields = true
};

string json = JsonSerializer.Serialize(p, options);



## Задача 2. 

а) public class Game
{
    public string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; }
    public bool IsMultiplayer { get; set; }
}

б) По умолчанию System.Text.Json чувствителен к регистру букв при десериализации. Поэтому ключ title и свойство Title могут не совпасть.

Чтобы решить эту проблему, нужно установить PropertyNameCaseInsensitive = true в JsonSerializerOptions. Тогда сериализатор будет игнорировать различия между заглавными и строчными буквами.
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

Game game = JsonSerializer.Deserialize<Game>(json, options);

в) Да, это корректный JSON-массив. Он содержит элементы разных типов: число, строку, логическое значение и null.

Для десериализации можно использовать массив типа JsonElement[], так как JsonElement позволяет представлять значения разных JSON-типов.



## Задача 3.

## Задача 3. File и FileStream

1. File — небольшой файл можно прочитать целиком с помощью `File.ReadAllText()`.
2. File — для автоматического сохранения прогресса игры можно использовать `File.WriteAllText()`.
3. FileStream — позволяет работать с большим файлом частями, не загружая его полностью в память.
4. File — для проверки существования используется `File.Exists()`, а для чтения всего файла — `File.ReadAllText()`.
5. FileStream — позволяет изменять данные в определённой позиции большого файла без полной перезаписи.