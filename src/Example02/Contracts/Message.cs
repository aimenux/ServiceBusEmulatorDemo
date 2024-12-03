namespace Example02.Contracts;

public sealed record Message
{
    public Guid Id { get; init; }
    public string Category { get; init; }
    public string Text { get; init; }

    public Message()
    {
        Id = Guid.NewGuid();
        Category = GetRandomCategory();
        Text = $"Text for Id {Id}";
    }

    private static string GetRandomCategory()
    {
        var category = Enum.GetValues<Categories>()
            .OrderBy(_ => Guid.NewGuid())
            .First();

        return category.ToString();
    }
}