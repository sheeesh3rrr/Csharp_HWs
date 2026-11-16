static class Program
{
    private static string[] _arr = [
"1. Добавить квест",
"2. Завершить квест",
"3. Провалить квест",
"4. Показать все квесты",
"5. Показать только активные квесты",
"6. Выход"];

    private static JournalService _journalService = new();

    static void Main()
    {
        while (true)
        {
            if (Enter(Drawer.GetTab(_arr)))
                break;
        }
    }

    static bool Enter(int index)
    {
        switch (index)
        {
            case 0:
                _journalService.AddQuest();
                break;
            case 1:
                _journalService.ChangeStatus("Completed");
                break;
            case 2:
                _journalService.ChangeStatus("Failed");
                break;
            case 3:
                _journalService.ShowQuests();
                break;
            case 4:
                _journalService.ShowQuests(onlyActive: true);
                break;
            case 5:
                return true;
        }

        return false;
    }
}

