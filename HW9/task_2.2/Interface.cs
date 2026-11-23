public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public enum Rarity
{
    Common,
    Rare,
    Legendary
}