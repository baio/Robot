using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Actors
{
    public interface IActorStorageEntity<T>
    {
        bool HasValue { get; }
        T Value { get; }
    }


    public interface IActorStorage<T>
    {
        Task<IActorStorageEntity<T>> Get();
        Task Set(T Val);
    }
}
