using System.Collections;
using UniRx;

namespace UdonObservable
{
    /// <summary>
    /// 他のTimer系は実装思いついたらやります
    /// </summary>
    public static class RealTimerRx
    {
        private static IEnumerator WaitForRealTimeCoroutine(float time)
        {
            yield return new UdonLib.Commons.WaitForSecondsRealtime(time);
        }

        public static System.IObservable<Unit> RealTimer(float time)
        {
            return WaitForRealTimeCoroutine(time)
                .ToObservable();
        }

    }
}
