using System.Threading.Tasks;
using System.Collections;
using UniRx.Async;

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
        UniTask Initialize();
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
        public virtual UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
    }
}