using MonopolyRoomServer.Services;
using Rubicks.Extensions;

namespace MonopolyRoomServer.CliCommands
{
    public class CliUser
    {
        private SubscriptionService _subscriptions;
        private IDisposable? _subscription;

        public CliUser(SubscriptionService subscriptions)
        {
            _subscriptions = subscriptions;
        }

        public bool IsSubscribed => _subscription != null;

        public void SubscribeToRoomChanges(Action<string> manipulateChanges)
        {
            if(_subscription != null)
            {
                throw new InvalidOperationException("Already subscribed");
            }
            _subscription = _subscriptions.Select(x => x.Serialize()).Subscribe(manipulateChanges);
        }

        public void UnsubscribeToRoomChanges()
        {
            if(_subscription == null)
            {
                throw new InvalidOperationException("Not subscribed");
            }
            _subscription.Dispose();
            _subscription = null;
        }
    }
}
