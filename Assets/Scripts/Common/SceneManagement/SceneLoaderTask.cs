using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Common.SceneManagement
{
    public class SceneLoaderTask
    {
        private readonly MonoBehaviour coroutineManager;
        private readonly string sceneName;
        private readonly LoadSceneMode loadSceneMode;
        private readonly Action<Scene> complete;

        public SceneLoaderTask(MonoBehaviour coroutineManager, string sceneName, LoadSceneMode loadSceneMode, Action<Scene> complete)
        {
            this.coroutineManager = coroutineManager;
            this.sceneName = sceneName;
            this.loadSceneMode = loadSceneMode;
            this.complete = complete;

            SceneManager.sceneLoaded += OnComplete;
        }

        private void OnComplete(Scene loadedScene, LoadSceneMode mode)
        {
            if (loadedScene.name == sceneName)
            {
                SceneManager.sceneLoaded -= OnComplete;
                complete?.Invoke(loadedScene);
            }
        }

        public void Start()
        {
            coroutineManager.StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var aop = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            yield return aop;
            
            stopwatch.Stop();
            Debug.Log($"Scene '{sceneName}' load time: {stopwatch.Elapsed}");
        }
    }
}