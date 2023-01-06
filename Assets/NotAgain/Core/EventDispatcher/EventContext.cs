using System;
using System.Collections.Generic;

namespace NotAgain.Core.EventDispatcher
{
    public class EventContext
    {
        Dictionary<IEvent, LinkedList<Delegate>> _eventDict = new();

        public void AddListener(IEvent ev, Action action)
        {
            if (!_eventDict.TryGetValue(ev, out LinkedList<Delegate> list))
            {
                list = new LinkedList<Delegate>();
                _eventDict.Add(ev, list);
            }

            list.AddLast(action);
        }

        public void AddListener<TArg>(OneArgEvent<TArg> ev, Action<TArg> action)
        {
            if (!_eventDict.TryGetValue(ev, out LinkedList<Delegate> list))
            {
                list = new LinkedList<Delegate>();
                _eventDict.Add(ev, list);
            }

            list.AddLast(action);
        }

        public void RemoveListener(IEvent ev, Action action)
        {
            if (!_eventDict.TryGetValue(ev, out LinkedList<Delegate> delegates))
            {
                return;
            }

            delegates.Remove(action);
        }

        public void RemoveListener<TArg>(OneArgEvent<TArg> ev, Action<TArg> action)
        {
            if (!_eventDict.TryGetValue(ev, out LinkedList<Delegate> delegates))
            {
                return;
            }

            delegates.Remove(action);
        }

        public void Invoke(IEvent ev)
        {
            if (!_eventDict.TryGetValue(ev, out LinkedList<Delegate> delegates))
            {
                return;
            }

            var node = delegates.First;
            while (node != null)
            {
                var next = node.Next;
                node.Value.DynamicInvoke();
                node = next;
            }
        }

        public void Invoke<TArg>(IEvent ev, TArg arg)
        {
            if (!_eventDict.TryGetValue(ev, out LinkedList<Delegate> delegates))
            {
                return;
            }

            var node = delegates.First;
            while (node != null)
            {
                var next = node.Next;
                node.Value.DynamicInvoke(arg);
                node = next;
            }
        }
    }
}