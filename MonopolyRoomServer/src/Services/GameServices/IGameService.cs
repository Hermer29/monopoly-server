using GameServerParts.Entities;
using MonopolyRoomServer.Entities;

namespace MonopolyRoomServer.Services
{
    public interface IGameService
    {
        void Initialize();
        void CreateGameFromRoom(Room room);
        bool IsPlaying(string playerId);

        bool IsPlaying(Player player)
        {
            return IsPlaying(player.Id);
        }
    }
}
