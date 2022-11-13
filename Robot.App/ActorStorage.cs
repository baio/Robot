using Robot.Actors;

namespace Robot.App
{
    internal readonly record struct ActorStorageEntity<T>(bool HasValue, T Value) : IActorStorageEntity<T>
    {
    }

    internal class ActorStorage<T> : IActorStorage<T>
    {
        private IActorStorageEntity<T> _stateEntity = new ActorStorageEntity<T>(false, default);

        internal ActorStorage() { }

        public Task<IActorStorageEntity<T>> Get()
        {
            return Task.FromResult(_stateEntity);
        }

        public Task Set(T Val)
        {
            _stateEntity = new ActorStorageEntity<T>(true, Val);

            return Task.FromResult(_stateEntity);
        }
    }
}
