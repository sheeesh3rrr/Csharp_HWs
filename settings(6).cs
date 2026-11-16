using System.Text.Json;

public class GameSettings
{
    public string PlayerName { get; set; }
    public int Difficulty { get; set; }
    public int MusicVolume { get; set; }

}

class Program
{
    static void Main(string[] args)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameSettings? settings = JsonSerializer.Deserialize<GameSettings>(json);

            if (settings != null)
            {
                Console.WriteLine("=== Текущие настройки игры ===");
                Console.WriteLine($"Имя игрока: {settings.PlayerName}");
                Console.WriteLine($"Сложность: {(Difficulty)settings.Difficulty} ({settings.Difficulty})");
                Console.WriteLine($"Громкость музыки: {settings.MusicVolume}%");
            }
        }
        else
        {
            Console.WriteLine("Файл настроек не найден. Создаём новые настройки.\n");

            GameSettings newSettings = new GameSettings();

            Console.Write("Введите имя игрока: ");
            newSettings.PlayerName = Console.ReadLine() ?? "Игрок"; /* если ничего не будет введено, то просто Игрок */

            newSettings.Difficulty = IntInRange("Выберите сложность (1 — легко, 2 — нормально, 3 — сложно): ", 1, 3);
            /* принимаем число от 1 до 3 */

            newSettings.MusicVolume = IntInRange("Введите громкость музыки (0–100): ", 0, 100);
            /* принимаем число от 0 до 100 */

            string jsonString = JsonSerializer.Serialize(newSettings, options);
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine("\nНастройки сохранены");
        }
    }

    static int IntInRange(string text, int min, int max)
    {
        while (true)
        {
            Console.Write(text);
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return Math.Clamp(value, min, max); /* чтобы не вылететь за границу */
            }
            Console.WriteLine($"Ошибка, это не целое число");
        }
    }

    
    public enum Difficulty
    {
        Легко = 1,
        Нормально = 2,
        Сложно = 3
    }
}

