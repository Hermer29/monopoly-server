using MonopolyGameServer.Game.ProcessPropertiesBorder;
using MonopolyGameServer.Game.Properties.MapFactories;

namespace MonopolyGameServer.Game.Properties;

public class FieldController
{
    private GameField? _field;

    public void HandleStep(IPlayerOnMap player)
    {
        BuildFieldIfNotBuilded();
        TriggerPassByEffects(player);
        
        var data = _field.GetStepData(player.NewPosition);

        if (data.IsSpecial)
        {
            data.ActionOnStep(player);
            return;
        }

        if (data.Owned && data.OwnerId != player.Id && data.Pledged == false)
        {
            player.Say(Rule.Rent, data.Rent.ToString());
        }

        if (data.Owned && data.OwnerId != player.Id && data.Pledged)
        {
            player.Say(Rule.StepOnPledged);
        }

        if (data.Owned == false)
        {
            player.Say(Rule.PlayerCanBuy, data.BuyCost.ToString());
        }
    }

    public void BuyCell(int position, BuyerData player)
    {
        _field.BuyCell(position, player);
    }

    private void TriggerPassByEffects(IPlayerOnMap player)
    {
        for (int position = player.LastPosition; position != player.NewPosition; position++)
        { 
            var intermediateData = _field.GetStepData(position);
            intermediateData.ActionOnPassBy?.Invoke(player);
        }
    }

    private void BuildFieldIfNotBuilded()
    {
        if (_field != null)
            return;
        _field = new SimpleFieldFactory().Build();
    }
}