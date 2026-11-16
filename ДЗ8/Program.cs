using System;

using System.IO;

using System.Text.Json;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main()
        {
            // ===== ЗАДАНИЕ 4 =====
            GameCharacter character = new GameCharacter
            {
                Name = "Alex",
                Level = 9,
                Health = 75.5,
                Inventory = new string[] { "Sword", "Shield", "Potion" }
            };

            
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            
            string json = JsonSerializer.Serialize(character, options);

            // ===== ЗАДАНИЕ 5 =====
            string filePath = Path.Combine(AppContext.BaseDirectory, "save.json");
            File.WriteAllText(filePath, json);
            string loadedJson = File.ReadAllText(filePath);
            GameCharacter loadedCharacter = JsonSerializer.Deserialize<GameCharacter>(loadedJson);

            Console.WriteLine("Загруженный персонаж:");
            Console.WriteLine("Имя: " + loadedCharacter.Name);
            Console.WriteLine("Уровень: " + loadedCharacter.Level);
            Console.WriteLine("Здоровье: " + loadedCharacter.Health);
            Console.WriteLine("Инвентарь: " + string.Join(", ", loadedCharacter.Inventory));

            Console.WriteLine("JSON:");
            Console.WriteLine(json);

            
            GameCharacter restoredCharacter = JsonSerializer.Deserialize<GameCharacter>(json);

            Console.WriteLine("\nВосстановленный персонаж:");
            Console.WriteLine("Имя: " + restoredCharacter.Name);
            Console.WriteLine("Количество предметов: " + restoredCharacter.Inventory.Length);





            // ===== ЗАДАНИЕ 6 =====
            string settingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");

            if (File.Exists(settingsPath))
            {
                Console.WriteLine("Сохранённые настройки:");

                string settingsJson = File.ReadAllText(settingsPath);

                GameSettings settings = JsonSerializer.Deserialize<GameSettings>(settingsJson);

                Console.WriteLine("Имя игрока: " + settings.PlayerName);
                Console.WriteLine("Сложность: " + settings.Difficulty);
                Console.WriteLine("Громкость музыки: " + settings.MusicVolume);
            }
            else
            {
                Console.WriteLine("Файл настроек не найден. Создаём новые настройки.");

                Console.Write("Введите имя игрока: ");
                string playerName = Console.ReadLine();

                Console.Write("Выберите сложность (1 — легко, 2 — нормально, 3 — сложно): ");
                int difficulty = int.Parse(Console.ReadLine());

                Console.Write("Введите громкость музыки (0–100): ");
                int musicVolume = int.Parse(Console.ReadLine());

                GameSettings newSettings = new GameSettings
                {
                    PlayerName = playerName,
                    Difficulty = difficulty,
                    MusicVolume = musicVolume
                };

                string newJson = JsonSerializer.Serialize(newSettings, options);

                File.WriteAllText(settingsPath, newJson);

                Console.WriteLine("Настройки успешно сохранены!");
            }

        }

        public class GameCharacter
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public double Health { get; set; }
            public string[] Inventory { get; set; }
        }
    }
    public class GameSettings
    {
        public string PlayerName { get; set; }
        public int Difficulty { get; set; }
        public int MusicVolume { get; set; }
    }
}
