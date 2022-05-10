using System.Net.Sockets;

namespace GameServerParts.Entities
{
    public class Player : Client, ISerializable
    {
        public Player(Socket client, string id) : base(client)
        {
            Subscribe();
            Id = id;
        }

        public Player(string id) : base()
        {
            Subscribe();
            Id = id;
        }

        public Player(Client client, string id) : base(client) 
        {
            Id = id;
        }

        public new event Action<Player>? Disconnected;

        public string Id { get; }

        private void Subscribe()
        {
            base.Disconnected += OnClientDisconnected;
        }

        private void OnClientDisconnected(Client client)
        {
            Disconnected?.Invoke(this);
        }

        public string Serialize()
        {
            return Id;
        }
    }
}
