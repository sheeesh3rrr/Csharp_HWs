задача 1.
а) в json попадут name и score, тк они публичные с get и set. secretcode не попадет в json, тк он приватный. isalive тоже не попадет , тк это поле , а system.text.json по умолчанию сериализует свойства.
б) можно использовать атрибут [jsoninclude] :
[JsonInclude]
private int secretCode = 42;
в) 1. использовать includefields:
var options = new JsonSerializerOptions
{
  IncludeFields = true
};
string json = JsonSerializer.Serialize(p, options);
2. преобразовать в публичное свойство:
public bool IsAlive { get; set; }

задача 2.
а) 
public class Game
{
  public string Title { get; set; } = "";
  public int Year { get; set; }
  public double Rating { get; set; }
  public string[] Genres { get; set; } = Array.Empty<string>();
  public bool IsMultiplayer { get; set; }
}
б) jsonserializer.deserialize слишком чувствителен к регистру.
var options = new JsonSerializerOptions
{
  PropertyNameCaseInsensitive = true
};
в) да, в массив объектов.

задача 3.
1. файл. тк этот файл небольшой, его можно легко и быстро прочитать с помощью file.readalltext
2. файл. тк данные перса можно сериализовать в строку и записать в файл с помощью file.writealltext
3. файлстрим. тк мы работаем с большими объемами данных , лучше использовать потоки
4. файл. для проверки можно использовать file.exists, для чтения содержимого - file.readalltext
5. файлстрим. так можно перемещаться по файлу с помощью seek и перезаписать определенные данные не перезаписывая весь файл целиком

пс. иногда мне было лень менять раскладку и регистр, надеюсь все понятно








