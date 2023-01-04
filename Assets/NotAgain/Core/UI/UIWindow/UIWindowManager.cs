using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace NotAgain.Core.UI.UIWindow
{
    public enum UIWindowID
    {
        INVALID_WINDOW
    }

    [Serializable]
    public class UIWindowIDUIWindowDictionary : SerializableDictionary<UIWindowID, Core.UI.UIWindow.UIWindow>
    {
    }

    public class UIWindowManager : MonoBehaviour
    {
        [SerializeField] UIWindowIDUIWindowDictionary _uiWindows;

        Dictionary<UIWindowID, Core.UI.UIWindow.UIWindow> _loadedWindows = new();
        Stack<Core.UI.UIWindow.UIWindow> _openedWindows;

        public UIWindowID CurrentWindowID =>
            _openedWindows.Count > 0 ? _openedWindows.Peek().UIWindowID : UIWindowID.INVALID_WINDOW;

        public Core.UI.UIWindow.UIWindow CurrentWindow =>
            _openedWindows.Count > 0 ? _openedWindows.Peek() : null;

        public async Task<TWindow> Open<TWindow>(UIWindowID windowID) where TWindow : Core.UI.UIWindow.UIWindow
        {
            if (windowID == UIWindowID.INVALID_WINDOW)
                return null;

            var window = await GetOrCreate(windowID);
            await CurrentWindow?.OnClose(windowID);
            await window.OnOpen(CurrentWindowID);
            _openedWindows.Push(window);
            
            return window as TWindow;
        }
        
        public async Task<TWindow> Switch<TWindow>(UIWindowID windowID) where TWindow : Core.UI.UIWindow.UIWindow
        {
            if (windowID == UIWindowID.INVALID_WINDOW)
                return null;

            while (CurrentWindowID != windowID || CurrentWindowID != UIWindowID.INVALID_WINDOW)
            {
                await CloseCurrent();
            }

            if (CurrentWindowID == windowID)
                return CurrentWindow as TWindow;
            
            return await Open<TWindow>(windowID);
        }
        
        public async Task CloseCurrent()
        {
            if (CurrentWindowID == UIWindowID.INVALID_WINDOW)
                return;
            
            var window = _openedWindows.Pop();
            await window.OnClose(CurrentWindowID);
            await CurrentWindow?.OnOpen(window.UIWindowID);
            if (!_loadedWindows.ContainsKey(window.UIWindowID))
            {
                await window.DeInitialize();
                _loadedWindows.Add(window.UIWindowID, window);
            }
        }
        
        public void CloseAllHard()
        {
            while (CurrentWindowID != UIWindowID.INVALID_WINDOW)
            {
                var window = _openedWindows.Pop();
                Destroy(window);
            }

            foreach (var (_, window) in _loadedWindows)
            {
                Destroy(window);
            }
            _loadedWindows.Clear();
        }

        async Task<Core.UI.UIWindow.UIWindow> GetOrCreate(UIWindowID windowID)
        {
            if (_loadedWindows.TryGetValue(windowID, out var window))
            {
                await window.Initialize();
                return window;
            }

            if (_uiWindows.TryGetValue(windowID, out var prefab))
            {
                var newWindow = Instantiate(prefab, transform);
                await newWindow.OnCreate(windowID);
                await newWindow.Initialize();
            }
            
            return null;
        }
    }
}