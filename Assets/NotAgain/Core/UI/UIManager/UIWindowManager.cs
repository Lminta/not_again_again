using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotAgain.UI.UIManager
{
    public enum UIWindowType
    {
    }

    [Serializable]
    public class UITypeUIWindowDictionary : SerializableDictionary<UIWindowType, UIWindow>
    {
    }

    public class UIWindowManager : MonoBehaviour
    {
        [SerializeField] UITypeUIWindowDictionary _uiWindows;
    }
}