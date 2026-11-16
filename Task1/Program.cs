using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class QuestEntry
{
    public string QuestName { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
}

public class AdventureJournal
{
    public string HeroName { get; set; }
    public List<QuestEntry> Quests { get; set; } = new List<QuestEntry>();
}

class Program
{
    static void Main()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "journal.json");
        AdventureJournal journal;

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            journal = JsonSerializer.Deserialize<AdventureJournal>(json) ?? new AdventureJournal();
            Console.WriteLine($"Журнал героя {journal.HeroName} успешно загружен.");
        }
        else
        {
            Console.Write("Файл журнала не найден. Введите имя героя: ");
            string heroName = Console.ReadLine();
            journal = new AdventureJournal
            {
                HeroName = heroName,
                Quests = new List<QuestEntry>()
            };
            SaveJournal(journal, filePath);
            Console.WriteLine("Создан новый журнал приключений.");
        }

        while (true)
        {
            Console.WriteLine("\nЖурнал приключений");
            Console.WriteLine("1. Добавить квест");
            Console.WriteLine("2. Завершить квест");
            Console.WriteLine("3. Провалить квест");
            Console.WriteLine("4. Показать все квесты");
            Console.WriteLine("5. Показать только активные квесты");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите пункт меню: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите название квеста: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите описание квеста: ");
                    string desc = Console.ReadLine();

                    journal.Quests.Add(new QuestEntry
                    {
                        QuestName = name,
                        Description = desc,
                        Status = "Active"
                    });
                    SaveJournal(journal, filePath);
                    Console.WriteLine("Квест успешно добавлен и сохранен!");
                    break;

                case "2":
                case "3":
                    string targetStatus = choice == "2" ? "Completed" : "Failed";
                    var activeQuests = journal.Quests.Where(q => q.Status == "Active").ToList();

                    if (activeQuests.Count == 0)
                    {
                        Console.WriteLine("Нет активных квестов.");
                        break;
                    }

                    Console.WriteLine("Активные квесты:");
                    for (int i = 0; i < activeQuests.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {activeQuests[i].QuestName} - {activeQuests[i].Description}");
                    }

                    Console.Write("Введите номер квеста: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= activeQuests.Count)
                    {
                        activeQuests[index - 1].Status = targetStatus;
                        SaveJournal(journal, filePath);
                        Console.WriteLine($"Статус квеста изменен на: {targetStatus}");
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер.");
                    }
                    break;

                case "4":
                    Console.WriteLine($"Все квесты героя {journal.HeroName}");
                    if (journal.Quests.Count == 0)
                    {
                        Console.WriteLine("Журнал пуст.");
                    }
                    else
                    {
                        foreach (var q in journal.Quests)
                        {
                            Console.WriteLine($"[{q.Status}] {q.QuestName}: {q.Description}");
                        }
                    }
                    break;

                case "5":
                    Console.WriteLine($"Активные квесты героя {journal.HeroName}");
                    var activeOnly = journal.Quests.Where(q => q.Status == "Active").ToList();
                    if (activeOnly.Count == 0)
                    {
                        Console.WriteLine("Нет активных квестов.");
                    }
                    else
                    {
                        foreach (var q in activeOnly)
                        {
                            Console.WriteLine($"[Active] {q.QuestName}: {q.Description}");
                        }
                    }
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
    }

    static void SaveJournal(AdventureJournal journal, string path)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(journal, options);
        File.WriteAllText(path, json);
    }
}