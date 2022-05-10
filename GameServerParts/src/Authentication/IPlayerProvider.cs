using GameServerParts.Entities;

namespace GameServerParts.Authentication
{
    public interface IPlayerProvider
    {
        public event Action<Player> PlayerLogged;
    }
}
