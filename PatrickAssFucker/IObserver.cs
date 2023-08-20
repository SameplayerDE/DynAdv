using System.Linq.Expressions;

namespace PatrickAssFucker
{
    public interface IObserver<T>
    {
        void Update(T message);
    }
}
