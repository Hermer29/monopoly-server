using System.Diagnostics;
using MonopolyGameServer.Game.Properties.Exceptions;

namespace MonopolyGameServer.Game.Properties;

public class Property : BuyableCell
{
    private Property[]? _dependents;
    private readonly PropertyData _data;

    public Property(PropertyData data)
    {
        _data = data;
    }
    
    public override int BuyCost { get; }
    public uint UpgradeLevel { get; private set; } = 0;

    public bool IsPartOfSet => _dependents != null && _dependents.All(x => x.Owner == Owner);

    protected override int SpecialRent
    {
        get
        {
            if (Owned == false || Pledged)
                return 0;
            return IsPartOfSet switch
            {
                false => _data.RentWithoutSet,
                true when UpgradeLevel == 0 => _data.RentWithUpgradeLevel[0],
                _ => _data.RentWithUpgradeLevel[UpgradeLevel]
            };
        }
    }

    public override int PledgeCost => _data.PledgeCost;

    public void Upgrade()
    {
        AssertIsGrouped();

        Debug.Assert(_dependents != null, nameof(_dependents) + " != null");
        if (_dependents.Select(x => x.UpgradeLevel).Any(x => x == UpgradeLevel - 1))
            throw new InconsistentUpgradeLevelChangeException("Upgrade other properties before");

        if (UpgradeLevel == (uint) Level.Hotel)
            throw new UpgradeLimitException("Already fully upgraded");
        
        UpgradeLevel++;
    }

    private void AssertIsGrouped()
    {
        if (_dependents == null)
            throw new InvalidOperationException(
                $"Properties should be united in groups of 2 or more properties ({nameof(Property.GroupUp)} method)");
    }

    public void Downgrade()
    {
        AssertIsGrouped();

        if (_dependents.Select(x => x.UpgradeLevel).Any((x => x == UpgradeLevel + 1)))
            throw new InconsistentUpgradeLevelChangeException("Downgrade other properties before");

        if (UpgradeLevel == (uint)Level.NoHouses)
            throw new UpgradeLimitException("Already fully downgraded");

        UpgradeLevel--;
    }

    public static void GroupUp(IEnumerable<Property> propertyGroup)
    {
        var properties = propertyGroup as Property[] ?? propertyGroup.ToArray();
        foreach (Property property in properties)
        {
            property.AddDependents(properties.Except(Enumerable.Repeat(property, 1)));
        }
    }

    private void AddDependents(IEnumerable<Property> property)
    {
        if (_dependents != null)
            throw new InvalidOperationException("Creating more groups of same properties not allowed");

        _dependents = property.ToArray();
    }
}