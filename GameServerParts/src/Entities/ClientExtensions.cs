using GameServerParts.Exceptions;

namespace GameServerParts.Entities
{
    public static class ClientExtensions
    {
        public static bool TryGetMessage(this Client client, out string message)
        {
            try
            {
                message = client.WaitForMessage();
            }
            catch (ClientDisconnectedException)
            {
                message = null;
                return false;
            }
            return true;
        }
    }
}
