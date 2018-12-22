using DG.Tweening;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UdonLib.Async
{
    public static class AwaitableDOTween
    {
        public static TaskAwaiter<Tween> GetAwaiter(this Tween self)
        {
            var source = new TaskCompletionSource<Tween>();

            TweenCallback onComplete = null;
            onComplete = () =>
            {
                source.SetResult(self);
            };

            return source.Task.GetAwaiter();
        }
    }
}
