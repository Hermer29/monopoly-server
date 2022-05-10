using MonopolyRoomServer.Entities;
using Rubicks.Extensions;
using Rubicks.Extensions.Disposing;

namespace MonopolyRoomServer.Services
{
    public class SubscriptionService : IObservable<Room>
    {
        private RoomService _rooms;
        private List<IObserver<Room>> _observers; 

        public SubscriptionService(RoomService rooms)
        {
            _rooms = rooms;
            _observers = new List<IObserver<Room>>();

            Subscribe();
        }

        private void Subscribe()
        {
            _rooms.RoomAdded += OnRoomChanged;
            _rooms.RoomUpdated += OnRoomChanged;
            _rooms.RoomRemoved += OnRoomChanged;
        }

        private void OnRoomChanged(Room room)
        {
            _observers.Notify(room);
        }

        public IDisposable Subscribe(IObserver<Room> observer)
        {
            _observers.Add(observer);
            return Disposable.Create(() => _observers.Remove(observer));
        }
    }
}
