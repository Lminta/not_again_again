using System.Threading;

namespace NotAgain.Utils
{
    public static class UnityThreadSync
    {
        public static int UnityThreadId { get; }


        static UnityThreadSync()
        {
            UnityThreadId = Thread.CurrentThread.ManagedThreadId;
        }


    }
}