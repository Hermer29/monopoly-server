using MonopolyGameServer.Game.ProcessPropertiesBorder;

namespace MonopolyGameServer.Game.Process;

public record TurnData(Response response, PlayerInGame.TurnHandler playerTurn, IEnumerable<PlayerInGame> playersInGroup, 
    FieldService field, PlayerInGame player);