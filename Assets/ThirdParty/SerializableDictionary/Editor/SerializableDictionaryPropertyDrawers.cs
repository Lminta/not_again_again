using NotAgain.Core.Scenes;
using NotAgain.UI.UIManager;
using UnityEditor;

[CustomPropertyDrawer(typeof(UIWindowIDUIWindowDictionary))]
[CustomPropertyDrawer(typeof(SceneIDNameDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

