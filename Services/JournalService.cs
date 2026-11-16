using System.Text.Json;

public class JournalService
{
    private readonly string _path = Path.Combine(AppContext.BaseDirectory, "journal.json");
    private readonly AdventureJournal _journal;

    public JournalService()
    {
        if (File.Exists(_path))
        {
            _journal = JsonSerializer.Deserialize<AdventureJournal>(File.ReadAllText(_path));
        }
        else
        {
            Console.Write("Введите имя героя: ");
            _journal = new AdventureJournal { HeroName = Console.ReadLine() };
            Save();
        }
    }

    public void AddQuest()
    {
        Console.Write("Введите название: ");
        string title = Console.ReadLine();
        Console.Write("Введите описание: ");
        string description = Console.ReadLine();

        _journal.Quests.Add(new QuestEntry { QuestName = title, Description = description, Status = "Active" });
        Save();
    }

    public void ShowQuests(bool onlyActive = false)
    {
        var quests = onlyActive ? _journal.Quests.Where(q => q.Status == "Active").ToList() : _journal.Quests;
        PrintQuests(quests);
        Console.WriteLine("Нажмите Enter для продолжения.");
        Console.ReadLine();
    }

    public void ChangeStatus(string status)
    {
        var activeQuests = _journal.Quests.Where(q => q.Status == "Active").ToList();
        if (activeQuests.Count == 0)
        {
            Console.WriteLine("Активных квестов нет. Нажмите Enter для продолжения.");
            Console.ReadLine();
            return;
        }

        PrintQuests(activeQuests);
        Console.Write("Введите номер квеста: ");
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= activeQuests.Count)
        {
            activeQuests[number - 1].Status = status;
            Save();
        }
        else
        {
            Console.WriteLine("Неверный номер. Нажмите Enter для продолжения.");
            Console.ReadLine();
        }
    }

    private static void PrintQuests(List<QuestEntry> quests)
    {
        if (quests.Count == 0)
            Console.WriteLine("Квестов нет.");

        for (int i = 0; i < quests.Count; i++)
        {
            var quest = quests[i];
            Console.WriteLine($"{i + 1}. {quest.QuestName} - {quest.Status}");
            Console.WriteLine($"\t{quest.Description}");
        }
    }

    private void Save()
    {
        File.WriteAllText(_path, JsonSerializer.Serialize(_journal));
    }
}
