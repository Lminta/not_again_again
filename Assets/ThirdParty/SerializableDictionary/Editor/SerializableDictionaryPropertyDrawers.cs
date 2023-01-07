using NotAgain.Core.Levels;
using NotAgain.Core.UI.UIWindow;
using UnityEditor;

[CustomPropertyDrawer(typeof(UIWindowIDUIWindowDictionary))]
[CustomPropertyDrawer(typeof(SceneIDNameDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

