using System;

namespace NotAgain.Core.UI.UINotify
{
    public enum NotifyType
    {
        NON_BLOCKING_MESSAGE,
        BLOCKING_MESSAGE
    }

    public struct NotifyData
    {
        public NotifyType Type;
        public string Title;
        public string Desc;
        public Action OnFirstKey;
        public Action OnSecondKey;
    }
}