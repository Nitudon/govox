using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace UdonLib.Commons
{
    public class Awaitable<T>
    {
        private readonly TaskCompletionSource<T> tcs = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Awaitable()
        {
            this.tcs = new TaskCompletionSource<T>();
        }

        /// <summary>
        /// 指定の処理を非同期に実行します。
        /// </summary>
        /// <param name="action">非同期に動かしたい処理</param>
        private void DoWorkAsync(Func<T> action)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try { this.tcs.SetResult(action()); }
                catch (Exception ex) { this.tcs.SetException(ex); }
            });
        }

        /// <summary>
        /// 対応するAwaiterを取得します。
        /// </summary>
        /// <returns>対応するAwaiter</returns>
        public virtual TaskAwaiter<T> GetAwaiter()
        {
            return this.tcs.Task.GetAwaiter();
        }

        /// <summary>
        /// 非同期処理の実行をラップします。
        /// </summary>
        /// <param name="action">非同期に動かしたい処理</param>
        /// <returns>await可能なオブジェクト</returns>
        public static Awaitable<T> Run(Func<T> action)
        {
            var awaitable = new Awaitable<T>();
            awaitable.DoWorkAsync(action);
            return awaitable;
        }
    }
}