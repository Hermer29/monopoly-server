namespace MonopolyGameServer.Game.Properties;

public class RailRoadCell : BuyableCell
{
    private RailRoadCell[]? _dependents;
    private RailRoadData _data;

    public RailRoadCell(RailRoadData data)
    {
        _data = data;
    }

    public override int BuyCost => _data.BuyCost;
    protected override int SpecialRent => _data.RentAccordingToBoughtCount[_dependents.Count(railRoad => railRoad.Owner == Owner) - 1];
    public override int PledgeCost => _data.PledgeCost;

    public static void GroupUp(IEnumerable<RailRoadCell> railRoadCells)
    {
        var roadCells = railRoadCells as RailRoadCell[] ?? railRoadCells.ToArray();
        foreach (var cell in roadCells)
        {
            cell.AddDependents(roadCells.Except(Enumerable.Repeat(cell, 1)));
        }
    }

    private void AddDependents(IEnumerable<RailRoadCell> dependents)
    {
        if (_dependents != null)
            throw new InvalidOperationException("Rail road already grouped up");
        _dependents = dependents.ToArray();
    }
}