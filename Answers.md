Часть 1

Задача 1
а) Сериализуются Name (т.к. это публичное свойство) и IsAlive (т.к. публичное поле). Поле secretCode не попадет, потому что оно private.
б) Надо написать атрибут [JsonInclude] прямо перед полем secretCode.
в) В коде задания стоит лишняя скобка `}` после IsAlive;. Из-за нее поле оказывается вне класса. Если убрать опечатку, поле попадет в json.

Задача 2
а) Класс будет таким:
public class Game
{
    public string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public string[] Genres { get; set; }
    public bool IsMultiplayer { get; set; }
}

б) Свойство Title написано с большой буквы, а ключ в json изменили на маленькую. По умолчанию сериализатор чувствителен к регистру. Чтобы починить, нужно при десериализации передать настройки JsonSerializerOptions, где указать PropertyNameCaseInsensitive = true.

в) Да, это валидный json (массив). В C# его можно десериализовать в object[].

Задача 3
1. File (File.ReadAllLines читает всё сразу).
2. File (File.WriteAllText быстро пишет готовую строку).
3. FileStream (чтобы дописывать логи в конец файла, не загружая гигабайты в память).
4. File (File.Exists для проверки и File.ReadAllText для чтения).
5. FileStream (можно сдвинуть указатель на нужный байт через Seek и перезаписать только кусочек).