using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SaveThosePrincesses.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly HashSet<SceneType> _loadedScenes;
        private Environment _loadedEnvironment;

        public SceneLoader()
        {
            _loadedScenes = new HashSet<SceneType>();
        }

        public bool IsLoaded(SceneType sceneType)
        {
            return _loadedScenes.Contains(sceneType);
        }

        public IEnumerator Load(SceneType sceneType)
        {
            if (IsLoaded(sceneType))
                yield break;
            
            _loadedScenes.Add(sceneType);
            yield return SceneManager.LoadSceneAsync(GetSceneName(sceneType), LoadSceneMode.Additive);
        }

        public IEnumerator Unload(SceneType sceneType)
        {
            if (!IsLoaded(sceneType))
                yield break;

            _loadedScenes.Remove(sceneType);
            yield return SceneManager.UnloadSceneAsync(GetSceneName(sceneType));
        }

        public IEnumerator Load(Environment environment)
        {
            if (_loadedEnvironment != null)
                yield return Unload(_loadedEnvironment);

            _loadedEnvironment = environment;
            yield return SceneManager.LoadSceneAsync(environment.SceneName);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(environment.SceneName));
        }

        public IEnumerator Unload(Environment environment)
        {
            if (_loadedEnvironment == environment)
                _loadedEnvironment = null;

            yield return SceneManager.UnloadSceneAsync(environment.SceneName);
        }
        
        private static string GetSceneName(SceneType sceneType)
        {
            return sceneType switch
            {
                SceneType.Menu => "menu",
                SceneType.Gameplay => "gameplay",
                SceneType.Services => "services",
                SceneType.UI => "ui",
                _ => throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null)
            };
        }
    }
}