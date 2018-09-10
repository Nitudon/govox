using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UdonLib.Commons {

    /// <summary>
    /// シーケンシャルにTaskを処理するやつ
    /// LiknkedListだとメモリ食うのでStackとQueueで打ち分け
    /// </summary>
    public class TaskSequence : IDisposable
    {
        public enum SequenceType
        {
            FIFO,
            FILO,
        }
    
        /// <summary>
        /// 非同期処理シーケンス
        /// </summary>
        private Queue<Task> _fifoSequence;
        private Stack<Task> _filoSequence;

        /// <summary>
        /// シーケンスのオーダー順
        /// </summary>
        private readonly SequenceType _orderType;

        public TaskSequence(SequenceType type = SequenceType.FIFO)
        {
            _orderType = type;

            if(type == SequenceType.FIFO)
            {
                _fifoSequence = new Queue<Task>();
            }
            else
            {
                _filoSequence = new Stack<Task>();
            }
        }

        /// <summary>
        /// 処理の追加
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(Task task)
        {
            if(_orderType == SequenceType.FIFO)
            {
                _fifoSequence.Enqueue(task);
            }
            else
            {
                _filoSequence.Push(task);
            }
        }

        /// <summary>
        /// シーケンス発火
        /// </summary>
        /// <returns></returns>
        public async Task ExcuteSequence()
        {
            if (_orderType == SequenceType.FIFO)
            {
                await ExecuteTaskQueue();
            }
            else
            {
                await ExecuteTaskStack();
            }
        }

        /// <summary>
        /// タスクキュー発火
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteTaskQueue()
        {
            while(_fifoSequence.Count > 0)
            {
                await _fifoSequence.Dequeue();
            }

            _fifoSequence.Clear();
        }

        /// <summary>
        /// スタックキュー発火
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteTaskStack()
        {
            while (_filoSequence.Count > 0)
            {
                await _filoSequence.Pop();
            }

            _fifoSequence.Clear();
        }

        /// <summary>
        /// シーケンス解放
        /// </summary>
        public void Dispose()
        {
            _fifoSequence.Clear();
            _filoSequence.Clear();
        }
    }
}
