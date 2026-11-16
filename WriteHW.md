№1
А) 
Name { get; set; }, тк у нас публичные свойства
Score { get; set; }, тк у нас публичные свойства

secretCode не попадет, тк он приватный
IsAlive не поадет, тк это поле. Его проигнорируют

Б)
Нужно его отметить специально, чтобы его увидел сериализатор:


```c#
using System.Text.Json.Serialization;
public class Player
{
	public string Name { get; set; }
	public int Score { get; set; }

	[JsonInclude]
	private int secretCode = 42;

	public bool IsAlive;
}
```

В)
1.Переделываем в свойство:
public bool IsAlive { get; set; }

2. Указываем специально: 
[JsonInclude]
public bool IsAlive;


№2
А)
Гланвое условие - типы и названия классов соответсвуют типам и названиям данных :

public class Game
{
    public string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; }
    public bool IsMultiplayer { get; set; }
}

Б) 
Тк по умолчанию сереализатор чувствителен к регистру символов.А мы заменили заглавную на
строчную, поэтому свойство получает значение null
Нужно передать в метод десериалезации объект с флагом нечувствивтельности:

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
var game = JsonSerializer.Deserialize<Game>(jsonString, options);

В) Да, валидна. Массив содержит разные типы, можно  десериализовать в массив object[]
или  в коллекцию List<object>

№3
1.File идеально для маленьких текстовых файлов, экономия на код.
2. File небольшой размер, используется часто, сильной обработки не нужно
3. File осилит, автоматически откроет, допишет, закроет. Размер все еще небольшой
4. File безопасный быстрый способ, экономия кода. Размер все еще небольшой
5. FileStream большой файл, нужент точечний доступ через позиционирование,
чтобы не загружать весь файл.

№4
смотреть в файле Programs.cs
