namespace MonopolyGameServer.Game.Properties;

public abstract class BuyableCell
{
    public int Rent => Owner == null ? 0 : SpecialRent;
    public Owner? Owner { get; private set; }
    public bool Pledged { get; private set; }
    public bool Owned => Owner != null;
    public abstract int BuyCost { get; }
    protected abstract int SpecialRent { get; }
    public abstract int PledgeCost { get; }
    
    public void Buy(Owner owner)
    {
        if (Owner != null)
            throw new InvalidOperationException();
        Owner = owner;
    }

    public void Pledge()
    {
        if (Pledged)
            throw new InvalidOperationException();
        Pledged = true;
    }

    public void Buyout()
    {
        if (Pledged == false)
            throw new InvalidOperationException();
        Pledged = false;
    }
}