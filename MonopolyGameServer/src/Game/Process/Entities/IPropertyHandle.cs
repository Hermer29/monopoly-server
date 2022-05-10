namespace MonopolyGameServer.Game.Process.Entities;

public interface IPropertyHandle
{
    void BuyoutCell(int position);
    void PledgeCell(int position);
    void BuyPositionCell(int position);
}