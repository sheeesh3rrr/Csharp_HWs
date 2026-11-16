public static class Drawer
{
    public static int GetTab(string[] tabs, string title = "=== Журнал приключений ===")
    {
        int selected = 0;
    
        while (true)
        {
            Console.Clear();
            Console.WriteLine(title);
            for (int i = 0; i < tabs.Length; i++)
            {
                if (selected == i)
                    Console.WriteLine(tabs[i] + " *");
                else
                    Console.WriteLine(tabs[i]);
            }

            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.DownArrow)
                selected += 1;
            else if (key.Key == ConsoleKey.UpArrow)
                selected -= 1;
            else if (key.Key == ConsoleKey.Enter)
                return selected;
            
            int n = tabs.Length;
            selected = ((selected % n) + n) % n;
        }
    }
}
