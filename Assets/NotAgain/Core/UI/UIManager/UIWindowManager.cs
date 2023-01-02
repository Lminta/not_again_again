using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotAgain.UI.UIManager
{
    public enum UIWindowID
    {
    }

    [Serializable]
    public class UIWindowIDUIWindowDictionary : SerializableDictionary<UIWindowID, UIWindow>
    {
    }

    public class UIWindowManager : MonoBehaviour
    {
        [SerializeField] UIWindowIDUIWindowDictionary _uiWindows;
    }
}