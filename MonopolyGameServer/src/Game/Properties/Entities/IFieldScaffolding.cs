namespace MonopolyGameServer.Game.Properties;

public interface IFieldScaffolding
{
    void AppendSpecialCell(SpecialCell cell);
    void AppendBuyableCell(BuyableCell cell, int groupId);

    GameField Build();
}