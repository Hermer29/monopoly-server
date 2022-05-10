using MonopolyGameServer.Game.Properties.Exceptions;

namespace MonopolyGameServer.Game.Properties;

public class GameField : IFieldScaffolding
{
    private List<(int position, SpecialCell cell)> _specialCells = new();
    private List<(int position, BuyableCell cell)> _buyableCells = new();
    private List<(BuyableCell cell, int groupId)> _cellGroups = new();
    private int _lastAddedCellIndex;

    public StepData GetStepData(int soughtPosition)
    {
        var cell = FindCell(soughtPosition);

        return cell switch
        {
            SpecialCell specialCell => new StepData(Position: soughtPosition, ActionOnStep: specialCell.EffectOnStep,
                ActionOnPassBy: specialCell.EffectOnPassBy, CanBuy: false, OwnerId: string.Empty, Owned: false,
                BuyCost: 0, Rent: 0, IsSpecial: true, Pledged: false),
            BuyableCell buyableCell => new StepData(Position: soughtPosition, ActionOnStep: null,
                CanBuy: buyableCell.Owned == false, BuyCost: 0, Owned: buyableCell.Owned, OwnerId: buyableCell.Owner?.Id ?? "",
                Rent: buyableCell.Rent, ActionOnPassBy: null, IsSpecial: false, Pledged: buyableCell.Pledged),
            _ => throw new InvalidOperationException("Unreachable")
        };
    }

    public void BuyCell(int position, BuyerData playerOnMap)
    {
        var cell = FindBuyableCell(position);

        if (cell.Owned) 
            throw new CellAlreadyBoughtException();
        
        if(playerOnMap.TurnData.playerTurn.TryRemoveCash(cell.BuyCost))
        {
            cell.Buy(new Owner(playerOnMap.Id));
            return;
        }
        throw new NotEnoughCashException();
    }

    private BuyableCell FindBuyableCell(int position)
    {
        BuyableCell cell;
        try { (_, cell) = _buyableCells.First(x => x.position == position); }
        catch (InvalidOperationException) { throw new CellNotFoundException();}

        return cell;
    }

    public void PledgeCell(int position, IPlayerOnMap playerOnMap)
    {
        var cell = FindBuyableCell(position);

        if (cell.Pledged)
            throw new CellAlreadyPledgedException();

        if (cell.Owned == false)
            throw new WrongCellActionException();
        
        playerOnMap.AddMoney(cell.PledgeCost, Rule.PlayerPledged);
    }

    private object FindCell(int soughtPosition)
    {
        foreach (var positionCellPair in 
                 _buyableCells.Where(positionCellPair => positionCellPair.position == soughtPosition))
        {
            return positionCellPair.cell;
        }

        foreach (var positionCellPair in 
                 _specialCells.Where(positionCellPair => positionCellPair.position == soughtPosition))
        {
            return positionCellPair.cell;
        }

        throw new ArgumentOutOfRangeException(nameof(soughtPosition));
    }
    
    void IFieldScaffolding.AppendSpecialCell(SpecialCell cell)
    {
        _specialCells.Add((_lastAddedCellIndex++, cell));
    }

    void IFieldScaffolding.AppendBuyableCell(BuyableCell cell, int groupId)
    {
        _cellGroups.Add((cell, groupId));
        _buyableCells.Add((_lastAddedCellIndex++, cell));
    }

    GameField IFieldScaffolding.Build()
    {
        foreach (var group in _cellGroups.GroupBy(x => x.groupId))
        {
            try
            {
                if (group.First().cell is Property)
                {
                    Property.GroupUp(group.Select(x => x.cell).Cast<Property>());
                }

                if (group.First().cell is RailRoadCell)
                {
                    RailRoadCell.GroupUp(group.Select(x => x.cell).Cast<RailRoadCell>());
                }
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException(
                    "Buyable cells of same type expect to be marked as same group id, cast exception detected");
            }
        }
        return this;
    }
}