using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace NotAgain.Core.Levels
{
    public enum SceneID
    {
        MAIN_GAME_SCENE,
        MINIGAME_SCENE
    }

    [Serializable]
    public class SceneIDNameDictionary : SerializableDictionary<SceneID, string>
    {
    }
    
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] SceneIDNameDictionary _scenes = new();
        
        Dictionary<SceneID, AsyncOperationHandle<SceneInstance>> _currentLoadOperations = new();

        Dictionary<SceneID, Scene> _loadedScenes = new();
        bool IsSceneLoaded(SceneID sceneID) => _loadedScenes.ContainsKey(sceneID);

        public async Task LoadScene(SceneID sceneID)
        {
            if (IsSceneLoaded(sceneID) || !_scenes.ContainsKey(sceneID))
                return;

            AsyncOperationHandle<SceneInstance> operation;

            if (_currentLoadOperations.ContainsKey(sceneID))
                operation = _currentLoadOperations[sceneID];
            else
            {
                operation = Addressables.LoadSceneAsync(_scenes[sceneID], LoadSceneMode.Additive);
                _currentLoadOperations.Add(sceneID, operation);
            }


            while (!operation.IsDone)
            {
                await new WaitForUpdate();
            }

            if (!_loadedScenes.ContainsKey(sceneID))
            {
                _loadedScenes.Add(sceneID, operation.Result.Scene);
            }

            _currentLoadOperations.Remove(sceneID);
        }

        public async Task UnloadScene(SceneID sceneID)
        {
            while (_currentLoadOperations.ContainsKey(sceneID))
            {
                await new WaitForUpdate();
            }

            if (!IsSceneLoaded(sceneID))
                return;
            
            await SceneManager.UnloadSceneAsync(_loadedScenes[sceneID]);

            _loadedScenes.Remove(sceneID);
        }
    }
}
