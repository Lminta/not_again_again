using System.Collections;
using System.Collections.Generic;
using NotAgain.UI.UIManager;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UITypeUIWindowDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

