using GameServerParts.Entities;

namespace MonopolyRoomServer.Entities
{
    public class PlayerInRoom : ISerializable
    {
        private Player _player;

        public PlayerInRoom(Player player)
        {
            _player = player;
        }

        public Player Player => _player;

        public void SayGoToGame()
        {
            _player?.TrySendMessage("gotogame");
        }

        public string Serialize() => _player.Serialize();
    }
}
