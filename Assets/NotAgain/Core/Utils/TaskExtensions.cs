using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace NotAgain.Utils
{
    public static class TaskExtensions
    {
        public static bool IsSuccess(this Task task)
        {
            if (task.Status == TaskStatus.Faulted)
            {
                if (task.Exception != null)
                {
                    foreach (var ex in task.Exception.InnerExceptions)
                    {
                        Debug.LogException(ex);
                    }
                }

                return false;
            }
            
            if (task.Status == TaskStatus.Canceled)
            {
                Debug.LogWarning("Task was canceled");
                return false;
            }

            return true;
        }

        public static async Task OnMainThread(this Task task, Action<bool> action)
        {
            await task;
            if (Thread.CurrentThread.ManagedThreadId != UnityThreadSync.UnityThreadId)
                await new WaitForEndOfFrame();
            action(task.IsSuccess());
        }

        public static async Task OnMainThread(this Task task, Action action)
        {
            await task;
            if (Thread.CurrentThread.ManagedThreadId != UnityThreadSync.UnityThreadId)
                await new WaitForEndOfFrame();
            task.IsSuccess();
            action();
        }

        public static async Task OnMainThread<TResult>(this Task<TResult> task, Action<Task<TResult>> action)
        {
            await task;
            if (Thread.CurrentThread.ManagedThreadId != UnityThreadSync.UnityThreadId)
                await new WaitForEndOfFrame();
            action(task);
        }

        public static async Task<TResult> WithResultOnMainThread<TResult>(this Task<TResult> task,
            Action<TResult> action)
        {
            await task;
            if (Thread.CurrentThread.ManagedThreadId != UnityThreadSync.UnityThreadId)
                await new WaitForEndOfFrame();
            var result = task.Result;
            action(result);
            return result;
        }

        public static async Task<TResultAfter> WithResultOnMainThread<TResultBefore, TResultAfter>(
            this Task<TResultBefore> task, Func<TResultBefore, TResultAfter> action)
        {
            await task;
            if (Thread.CurrentThread.ManagedThreadId != UnityThreadSync.UnityThreadId)
                await new WaitForEndOfFrame();
            var result = task.Result;
            return action(result);
        }

    }
}