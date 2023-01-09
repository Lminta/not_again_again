using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace NotAgain.Core.UI.UIWindow
{
    public enum UIWindowID
    {
        INVALID_WINDOW,
        MAIN_MENU,
        MAIN_GAME
    }

    [Serializable]
    public class UIWindowIDUIWindowDictionary : SerializableDictionary<UIWindowID, UIWindow>
    {
    }

    public class UIWindowManager : MonoBehaviour
    {
        [SerializeField] Canvas _uiCanvas;
        [SerializeField] UIWindowIDUIWindowDictionary _uiWindows = new();

        Dictionary<UIWindowID, UIWindow> _loadedWindows = new();
        Stack<UIWindow> _openedWindows = new ();

        public UIWindowID CurrentWindowID =>
            _openedWindows.Count > 0 ? _openedWindows.Peek().UIWindowID : UIWindowID.INVALID_WINDOW;

        public UIWindow CurrentWindow =>
            _openedWindows.Count > 0 ? _openedWindows.Peek() : null;

        public async Task<TWindow> Open<TWindow>(UIWindowID windowID) where TWindow : UIWindow
        {
            if (windowID == UIWindowID.INVALID_WINDOW)
                return null;

            var window = await GetOrCreate(windowID);
            if (CurrentWindowID != UIWindowID.INVALID_WINDOW)
                await CurrentWindow.OnClose(windowID);
            await window.OnOpen(CurrentWindowID);
            _openedWindows.Push(window);
            
            return window as TWindow;
        }
        
        public async Task<TWindow> Switch<TWindow>(UIWindowID windowID) where TWindow : UIWindow
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
        
        public async Task CloseCurrent(UIWindowID windowID = UIWindowID.INVALID_WINDOW)
        {
            if (CurrentWindowID == UIWindowID.INVALID_WINDOW)
                return;

            if (windowID != UIWindowID.INVALID_WINDOW && windowID != CurrentWindowID)
                return;
            
            var window = _openedWindows.Pop();
            await window.OnClose(CurrentWindowID);
            if (CurrentWindowID != UIWindowID.INVALID_WINDOW)
                await CurrentWindow.OnOpen(window.UIWindowID);
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

        async Task<UIWindow> GetOrCreate(UIWindowID windowID)
        {
            if (_loadedWindows.TryGetValue(windowID, out var window))
            {
                await window.Initialize();
                return window;
            }

            if (_uiWindows.TryGetValue(windowID, out var prefab))
            {
                var newWindow = Instantiate(prefab, _uiCanvas.transform);
                await newWindow.OnCreate(windowID);
                await newWindow.Initialize();
                return newWindow;
            }
            
            return null;
        }
    }
}