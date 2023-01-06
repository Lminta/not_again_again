using System;
using System.Collections.Generic;

namespace NotAgain.Core.EventDispatcher
{
    public class EventDispatcher
    {
        static  Dictionary<object, EventContext> _contexts = new ();
        static object _staticContextHolder = new ();
        static List<object> _nullKeys = new ();

        public static EventContext GetContext<TObject>(TObject obj)
        {
            if (!_contexts.TryGetValue(obj, out EventContext context))
            {
                context = new EventContext();
                _contexts.Add(obj, context);
            }

            return context;
        }

        public static void DestroyContext<TObject>(TObject obj)
        {
            _contexts.Remove(obj);
        }

        public static void AddListener(IEvent ev, Action action)
        {
            if (!_contexts.TryGetValue(_staticContextHolder, out EventContext context))
            {
                context = new EventContext();
                _contexts.Add(_staticContextHolder, context);
            }

            context.AddListener(ev, action);
        }
        
        public static void AddListener<TArg>(OneArgEvent<TArg> ev, Action<TArg> action)
        {
            if (!_contexts.TryGetValue(_staticContextHolder, out EventContext context))
            {
                context = new EventContext();
                _contexts.Add(_staticContextHolder, context);
            }

            context.AddListener(ev, action);
        }

        public static void RemoveListener(IEvent ev, Action action)
        {
            if (!_contexts.TryGetValue(_staticContextHolder, out EventContext context))
            {
                return;
            }

            context.RemoveListener(ev, action);
        }
        
        public static void RemoveListener<TArg>(OneArgEvent<TArg> ev, Action<TArg> action)
        {
            if (!_contexts.TryGetValue(_staticContextHolder, out EventContext context))
            {
                return;
            }

            context.RemoveListener(ev, action);
        }

        public static void Invoke(IEvent ev)
        {
            foreach (var kvp in _contexts)
            {
                if (kvp.Key == null)
                {
                    _nullKeys.Add(kvp.Key);
                    continue;
                }

                kvp.Value.Invoke(ev);
            }

            foreach (var key in _nullKeys)
            {
                _contexts.Remove(key);
            }
            _nullKeys.Clear();
        }

        public static void Invoke<TArg>(OneArgEvent<TArg> ev, TArg arg)
        {
            foreach (var kvp in _contexts)
            {
                if (kvp.Key == null)
                {
                    _nullKeys.Add(kvp.Key);
                    continue;
                }

                kvp.Value.Invoke(ev, arg);
            }

            foreach (var key in _nullKeys)
            {
                _contexts.Remove(key);
            }
            _nullKeys.Clear();
        }
    }
}