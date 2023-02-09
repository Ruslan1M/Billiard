using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner m_CoroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => m_CoroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            m_CoroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(name);

            while (!asyncLoadScene.isDone)
            {
                yield return null;
            }
            onLoaded?.Invoke();
        }
    }
}