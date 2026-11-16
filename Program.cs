using System.Text.Json;

static class Program
{
    private static readonly string LogPath = Path.Combine(AppContext.BaseDirectory, "actions.log");

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("=== Управление профилями ===");
            Console.WriteLine("1. Создать профиль");
            Console.WriteLine("2. Записать результат игры");
            Console.WriteLine("3. Показать профиль");
            Console.WriteLine("4. Показать лог действий");
            Console.WriteLine("5. Выход");

            switch (Console.ReadLine())
            {
                case "1": CreateProfile(); break;
                case "2": RecordResult(); break;
                case "3": ShowProfile(); break;
                case "4": ShowLog(); break;
                case "5": return;
                case null: return;
                default: Console.WriteLine("Неверный пункт меню."); break;
            }
        }
    }

    private static string ReadNickname()
    {
        Console.Write("Введите никнейм: ");
        string nickname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nickname) || nickname.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            Console.WriteLine("Некорректный никнейм.");
            return null;
        }
        return nickname;
    }

    private static string ProfilePath(string nickname) =>
        Path.Combine(AppContext.BaseDirectory, $"profile_{nickname}.json");

    private static void SaveProfile(PlayerProfile profile) =>
        File.WriteAllText(ProfilePath(profile.Nickname), JsonSerializer.Serialize(profile));

    private static PlayerProfile LoadProfile(string nickname)
    {
        string path = ProfilePath(nickname);
        if (!File.Exists(path))
        {
            Console.WriteLine("Профиль не найден");
            return null;
        }
        return JsonSerializer.Deserialize<PlayerProfile>(File.ReadAllText(path));
    }

    private static void Log(string message) =>
        File.AppendAllText(LogPath, $"[{DateTime.Now:dd.MM.yyyy HH:mm}] {message}{Environment.NewLine}");

    private static void CreateProfile()
    {
        string nickname = ReadNickname();
        if (nickname == null) return;
        if (File.Exists(ProfilePath(nickname)))
        {
            Console.WriteLine("Профиль уже существует!");
            return;
        }

        SaveProfile(new PlayerProfile { Nickname = nickname });
        Log($"Создан профиль: {nickname}");
    }

    private static void RecordResult()
    {
        string nickname = ReadNickname();
        if (nickname == null) return;
        PlayerProfile profile = LoadProfile(nickname);
        if (profile == null) return;

        Console.Write("Введите результат (win или loss): ");
        string result = Console.ReadLine();
        if (result != "win" && result != "loss")
        {
            Console.WriteLine("Неверный результат.");
            return;
        }

        profile.GamesPlayed++;
        if (result == "win") profile.Wins++;
        profile.WinRate = profile.Wins * 100.0 / profile.GamesPlayed;
        SaveProfile(profile);
        Log($"{nickname}: {result} (Всего игр: {profile.GamesPlayed}, Побед: {profile.Wins}, WinRate: {profile.WinRate:F1}%)");
    }

    private static void ShowProfile()
    {
        string nickname = ReadNickname();
        if (nickname == null) return;
        PlayerProfile profile = LoadProfile(nickname);
        if (profile == null) return;
        Console.WriteLine($"Никнейм: {profile.Nickname}");
        Console.WriteLine($"Всего игр: {profile.GamesPlayed}");
        Console.WriteLine($"Побед: {profile.Wins}");
        Console.WriteLine($"WinRate: {profile.WinRate:F1}%");
    }

    private static void ShowLog()
    {
        if (File.Exists(LogPath))
            Console.Write(File.ReadAllText(LogPath));
        else
            Console.WriteLine("Лог действий пуст.");
    }
}
