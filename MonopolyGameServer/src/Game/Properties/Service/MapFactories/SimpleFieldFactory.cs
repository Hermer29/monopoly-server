namespace MonopolyGameServer.Game.Properties.MapFactories;

public class SimpleFieldFactory : IFieldFactory
{
    private static readonly ChanceCell _cachedChanceCell;

    static SimpleFieldFactory()
    {
        _cachedChanceCell = new ChanceCell(
            new IChanceCellEvent[]
                {
                    new MedicalInsuranceEvent()
                }
            );
    }
    
    public GameField Build()
    {
        var builder = new FieldBuilder();
        var fieldData = new SimpleFieldData();
        
        builder.AppendSpecialCell(new StartCell(200));
        builder.AppendBuyableCell(new Property(fieldData.Property11), 1);
        builder.AppendSpecialCell(_cachedChanceCell);
        builder.AppendBuyableCell(new Property(fieldData.Property12), 1);
        builder.AppendSpecialCell(new TaxCell(fieldData.TaxCellCost));
        builder.AppendBuyableCell(new RailRoadCell(fieldData.RailRoadData), 10);
        builder.AppendBuyableCell(new Property(fieldData.Property21), 2);
        builder.AppendSpecialCell(_cachedChanceCell);
        builder.AppendBuyableCell(new Property(fieldData.Property22), 2);
        builder.AppendBuyableCell(new Property(fieldData.Property23), 2);
        builder.AppendSpecialCell(new PrisonCell());
        builder.AppendBuyableCell(new Property(fieldData.Property31), 3);
        builder.AppendBuyableCell(new Property(fieldData.Property41), 4);
        builder.AppendBuyableCell(new Property(fieldData.Property32), 3);
        builder.AppendBuyableCell(new Property(fieldData.Property33), 3);
        builder.AppendBuyableCell(new RailRoadCell(fieldData.RailRoadData), 10);
        builder.AppendBuyableCell(new Property(fieldData.Property51), 5);
        builder.AppendSpecialCell(_cachedChanceCell);
        builder.AppendBuyableCell(new Property(fieldData.Property52), 5);
        builder.AppendBuyableCell(new Property(fieldData.Property53), 5);
        builder.AppendSpecialCell(new CasinoCell());
        builder.AppendBuyableCell(new Property(fieldData.Property61), 6);
        builder.AppendSpecialCell(_cachedChanceCell);
        builder.AppendBuyableCell(new Property(fieldData.Property62), 6);
        builder.AppendBuyableCell(new Property(fieldData.Property63), 6);
        builder.AppendBuyableCell(new RailRoadCell(fieldData.RailRoadData), 10);
        builder.AppendBuyableCell(new Property(fieldData.Property71), 7);
        builder.AppendBuyableCell(new Property(fieldData.Property72), 7);
        builder.AppendBuyableCell(new Property(fieldData.Property42), 4);
        builder.AppendBuyableCell(new Property(fieldData.Property73), 7);
        builder.AppendSpecialCell(new GoToJailCell(10));
        builder.AppendBuyableCell(new Property(fieldData.Property81), 8);
        builder.AppendBuyableCell(new Property(fieldData.Property82), 8);
        builder.AppendSpecialCell(_cachedChanceCell);
        builder.AppendBuyableCell(new Property(fieldData.Property83), 8);
        builder.AppendBuyableCell(new RailRoadCell(fieldData.RailRoadData), 10);
        builder.AppendSpecialCell(new TaxCell(fieldData.TaxCellCost));
        builder.AppendBuyableCell(new Property(fieldData.Property91), 9);
        builder.AppendSpecialCell(_cachedChanceCell);
        builder.AppendBuyableCell(new Property(fieldData.Property92), 9);

        return builder.Build();
    }
}