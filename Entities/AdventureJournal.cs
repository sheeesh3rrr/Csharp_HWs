public class AdventureJournal
{
    public string HeroName { get; set; }
    public List<QuestEntry> Quests { get; set; } = new();
}
