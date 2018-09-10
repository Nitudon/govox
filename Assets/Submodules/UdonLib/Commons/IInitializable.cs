using System.Threading.Tasks;
using System.Collections;

namespace UdonLib.Commons
{
    public interface IInitializable
    {
        void Initialize();
    }

    public interface IInitializable<T>
    {
        void Initialize(T dependency);
    }

    public interface IYieldInitializable
    {
        IEnumerator Initialize();
    }

    public interface IAsyncInitializable
    {
        Task Initialize();
    }

    public abstract class InitializableMono : UdonBehaviour, IInitializable
    {
        public abstract void Initialize();
    }

    public abstract class InitializableMono<T> : UdonBehaviour, IInitializable<T>
    {
        public abstract void Initialize(T dependency);
    }

    public abstract class YieldInitializableMono : UdonBehaviour, IYieldInitializable
    {
        public virtual IEnumerator Initialize()
        {
            yield break;
        }
    }

    public abstract class AsyncInitializableMono : UdonBehaviour, IAsyncInitializable
    {
        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}