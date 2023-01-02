using System.Collections.Generic;
using System.Threading.Tasks;
using NotAgain.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace NotAgain.Core.Scenes
{
    public class ScenesManager : MonoSingleton<ScenesManager>
    {
        Dictionary<string, AsyncOperationHandle<SceneInstance>> _currentLoadOperations = new();

        Dictionary<string, Scene> _loadedScenes = new();
        bool IsSceneLoaded(string sceneName) => _loadedScenes.ContainsKey(sceneName);

        public async Task LoadScene(string sceneName)
        {
            if (IsSceneLoaded(sceneName))
                return;

            AsyncOperationHandle<SceneInstance> operation;

            if (_currentLoadOperations.ContainsKey(sceneName))
                operation = _currentLoadOperations[sceneName];
            else
            {
                operation = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                _currentLoadOperations.Add(sceneName, operation);
            }


            while (!operation.IsDone)
            {
                await new WaitForUpdate();
            }

            if (!_loadedScenes.ContainsKey(sceneName))
            {
                _loadedScenes.Add(sceneName, operation.Result.Scene);
            }

            _currentLoadOperations.Remove(sceneName);
        }

        public async Task UnloadScene(string sceneName)
        {
            while (_currentLoadOperations.ContainsKey(sceneName))
            {
                await new WaitForUpdate();
            }

            if (!IsSceneLoaded(sceneName))
                return;
            
            await SceneManager.UnloadSceneAsync(_loadedScenes[sceneName]);

            _loadedScenes.Remove(sceneName);
        }
    }
}
