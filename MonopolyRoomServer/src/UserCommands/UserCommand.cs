using GameServerParts;
using GameServerParts.Entities;
using MonopolyRoomServer.Channels;
using MonopolyRoomServer.Exceptions;

namespace MonopolyRoomServer.Commands
{
    public abstract class UserCommand
    {
        public void TryExecute(CommandText command, Player client, ChannelsAggregator channels)
        {
            try
            {
                if (command == null)
                {
                    throw new ArgumentNullException(nameof(command));
                }
                if (channels == null)
                {
                    throw new ArgumentNullException(nameof(channels));
                }
                if (client == null)
                {
                    throw new ArgumentNullException(nameof(client));
                }
            }
            catch (ArgumentNullException outer)
            {
                throw new MonopolyRoomServerException($"Exception was thrown in {GetType()}", outer);
            }
            
            OnTryExecute(command, client, channels);
        }

        protected abstract void OnTryExecute(CommandText command, Player client, ChannelsAggregator channels);
    }
}
