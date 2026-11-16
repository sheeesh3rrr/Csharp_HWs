using System;
using System.IO;
using System.Text.Json;

public class PlayerProfile
{
    public string Nickname { get; set; }
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public double WinRate { get; set; }
}

class Program
{
    static void Main()
    {
        string logPath = Path.Combine(AppContext.BaseDirectory, "actions.log");

        void LogAction(string message)
        {
            string logEntry = $"[{DateTime.Now:dd.MM.yyyy HH:mm}] {message}\n";
            File.AppendAllText(logPath, logEntry);
        }

        while (true)
        {
            Console.WriteLine("\nУправление профилями");
            Console.WriteLine("1. Создать профиль");
            Console.WriteLine("2. Записать результат игры");
            Console.WriteLine("3. Показать профиль");
            Console.WriteLine("4. Показать лог действий");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите пункт меню: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите никнейм: ");
                    string createNickname = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(createNickname)) break;

                    string profilePath = Path.Combine(AppContext.BaseDirectory, $"profile_{createNickname}.json");

                    if (File.Exists(profilePath))
                    {
                        Console.WriteLine("Профиль уже существует!");
                    }
                    else
                    {
                        var profile = new PlayerProfile
                        {
                            Nickname = createNickname,
                            GamesPlayed = 0,
                            Wins = 0,
                            WinRate = 0.0
                        };

                        var options = new JsonSerializerOptions { WriteIndented = true };
                        File.WriteAllText(profilePath, JsonSerializer.Serialize(profile, options));
                        
                        Console.WriteLine("Профиль успешно создан.");
                        LogAction($"Создан профиль: {createNickname}");
                    }
                    break;

                case "2":
                    Console.Write("Введите никнейм: ");
                    string gameNickname = Console.ReadLine()?.Trim();
                    string gameProfilePath = Path.Combine(AppContext.BaseDirectory, $"profile_{gameNickname}.json");

                    if (!File.Exists(gameProfilePath))
                    {
                        Console.WriteLine("Профиль не найден.");
                        break;
                    }

                    Console.Write("Введите результат игры (win / loss): ");
                    string result = Console.ReadLine()?.Trim().ToLower();

                    if (result != "win" && result != "loss")
                    {
                        Console.WriteLine("Некорректный ввод результата. Используйте 'win' или 'loss'.");
                        break;
                    }

                    string jsonRead = File.ReadAllText(gameProfilePath);
                    var player = JsonSerializer.Deserialize<PlayerProfile>(jsonRead);

                    player.GamesPlayed++;
                    if (result == "win")
                    {
                        player.Wins++;
                    }
                    player.WinRate = player.GamesPlayed > 0 ? (player.Wins * 100.0 / player.GamesPlayed) : 0.0;

                    var saveOptions = new JsonSerializerOptions { WriteIndented = true };
                    File.WriteAllText(gameProfilePath, JsonSerializer.Serialize(player, saveOptions));

                    Console.WriteLine("Результат игры успешно записан.");
                    LogAction($"{player.Nickname}: {result} (Всего игр: {player.GamesPlayed}, Побед: {player.Wins}, WinRate: {player.WinRate:F1}%)");
                    break;

                case "3":
                    Console.Write("Введите никнейм: ");
                    string showNickname = Console.ReadLine()?.Trim();
                    string showProfilePath = Path.Combine(AppContext.BaseDirectory, $"profile_{showNickname}.json");

                    if (!File.Exists(showProfilePath))
                    {
                        Console.WriteLine("Профиль не найден");
                    }
                    else
                    {
                        string profileJson = File.ReadAllText(showProfilePath);
                        var profileData = JsonSerializer.Deserialize<PlayerProfile>(profileJson);

                        Console.WriteLine($"Профиль: {profileData.Nickname}");
                        Console.WriteLine($"Всего игр: {profileData.GamesPlayed}");
                        Console.WriteLine($"Побед: {profileData.Wins}");
                        Console.WriteLine($"Процент побед (WinRate): {profileData.WinRate:F1}%");
                    }
                    break;

                case "4":
                    Console.WriteLine("Лог действий (actions.log)");
                    if (File.Exists(logPath))
                    {
                        string logContent = File.ReadAllText(logPath);
                        Console.Write(logContent);
                    }
                    else
                    {
                        Console.WriteLine("Лог пуст или еще не создан.");
                    }
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
    }
}