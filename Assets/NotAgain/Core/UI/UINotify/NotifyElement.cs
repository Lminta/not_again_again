using System;
using System.Threading.Tasks;
using UnityEngine;

namespace NotAgain.Core.UI.UINotify
{
    public abstract class NotifyElement : MonoBehaviour
    {
        public NotifyData Data { get; private set; }
        TaskCompletionSource<bool> _source;

        public virtual Task Show(NotifyData data)
        {
            Data = data;
            if (_source != null)
                return Task.FromException(new Exception("This notify element busy now"));
            _source = new TaskCompletionSource<bool>();
            gameObject.SetActive(true);
            return _source.Task;
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            _source.TrySetResult(true);
            _source = null;
        }

    }
}