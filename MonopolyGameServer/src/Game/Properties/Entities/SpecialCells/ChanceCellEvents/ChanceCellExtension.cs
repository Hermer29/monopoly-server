namespace MonopolyGameServer.Game.Properties.ChanceCellEvents;

public static class ChanceCellExtension
{
    public static IChanceCellEvent Random(this IEnumerable<IChanceCellEvent> cellEvents)
    {
        if (cellEvents == null)
            throw new ArgumentNullException(nameof(cellEvents));
        var cellEventsCount = cellEvents.Count();
        var randomIndex = System.Random.Shared.Next(0, cellEventsCount);
        return cellEvents.ElementAt(randomIndex);
    }
}