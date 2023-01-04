using System.Collections.Generic;
using System.Threading.Tasks;
using NotAgain.Utils;
using UnityEngine;

namespace NotAgain.Core.UI.UINotify
{
    public class UINotifyManager : MonoBehaviour
    {
        [SerializeField] NotifyElement _blockingNotify;
        [SerializeField] NotifyElement _nonBlockingNotify;
        
        NotifyElement _current;
        bool _pauseNonblocking = false;
        bool _pauseBlocking = false;
        Queue<NotifyData> _nonblockingQueue = new();
        Stack<NotifyData> _blockingStack = new();

        public void ShowMessage(NotifyData data)
        {
            switch (data.Type)
            {
                case NotifyType.NON_BLOCKING_MESSAGE:
                    EnqueueNonblockingMessage(data);
                    break;
                case NotifyType.BLOCKING_MESSAGE:
                    PushBlockingMessage(data);
                    break;
                default:
                    break;
            }

        }

        public void Clear()
        {
            _nonblockingQueue.Clear();
            _blockingStack.Clear();
            _current?.Close();
        }

        void PushBlockingMessage(NotifyData data)
        {
            _pauseNonblocking = true;
            _blockingStack.Push(data);
        }

        void CheckBlockingStack()
        {
            if (_pauseBlocking || _blockingStack.Count == 0 ||
                (_current != null && _current.Data.Type == NotifyType.BLOCKING_MESSAGE))
            {
                return;
            }

            _current?.Close();
            _current = _blockingNotify;
            ShowBlockingMessage(_blockingStack.Peek()).IsSuccess();
        }

        async Task ShowBlockingMessage(NotifyData data)
        {
            await _blockingNotify.Show(data);
            if (_blockingStack.Peek().Equals(data))
            {
                _blockingStack.Pop();
            }

            _current = null;
            if (_blockingStack.Count == 0)
            {
                _pauseNonblocking = false;
            }
        }

        void EnqueueNonblockingMessage(NotifyData data)
        {
            _nonblockingQueue.Enqueue(data);
        }

        void CheckNonblockingQueue()
        {
            if (_pauseNonblocking || _nonblockingQueue.Count == 0 || _current != null) return;
            _current = _nonBlockingNotify;
            ShowNotifyMessage(_nonblockingQueue.Dequeue()).IsSuccess();
        }

        async Task ShowNotifyMessage(NotifyData data)
        {
            await _nonBlockingNotify.Show(data);
            _current = null;
        }

        void Update()
        {
            CheckBlockingStack();
            CheckNonblockingQueue();
        }
    }
}