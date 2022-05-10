namespace MonopolyGameServer.Preparations
{
    public class GameData
    {
        public GameData(string name, string id, int playerAmount, params string[] playerIds)
        {
            Name = name;
            Id = id;
            PlayersIds = playerIds;
            PlayerAmount = playerAmount;
        }

        public string Name { get; }
        public string Id { get; }
        public string[] PlayersIds { get; }
        public int PlayerAmount { get; }

        public static GameData Deserialize(string text)
        {
            var args = text.Split(';');
            var name = args[0];
            var id = args[1];
            var playerAmount = args[2];
            var players = args.Skip(4).ToArray();
            return new GameData(name, id, int.Parse(playerAmount), players);
        }
    }
}
