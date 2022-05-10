namespace MonopolyGameServer.Game.Properties;

public class FieldBuilder : IFieldScaffolding
{
    private IFieldScaffolding _field;
    
    public FieldBuilder()
    {
        _field = new GameField();
    }

    public void AppendSpecialCell(SpecialCell cell)
    {
        _field.AppendSpecialCell(cell);
    }

    public void AppendBuyableCell(BuyableCell cell, int groupId)
    {
        _field.AppendBuyableCell(cell, groupId);
    }

    public GameField Build()
    {
        return _field.Build();
    }
}