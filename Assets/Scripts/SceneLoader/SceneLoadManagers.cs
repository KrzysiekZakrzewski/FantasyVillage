using Game.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.SceneLoader
{
    public class SceneLoadManagers : MonoBehaviour
    {
        [SerializeField]
        private LoadingViewController loadingViewController;

        private readonly HashSet<string> loadedLocationRelatedScenes = new HashSet<string>();
        private Coroutine loadingSceneCoroutine;

        private Queue<AsyncOperation> asyncOperations;

        public static bool IsLoading = false;

        public SceneDataSo CurrentLocationSceneDataSo { get; private set; }

        public event Action OnStartSceneLoaded;
        public event Action OnSceneLoaded;
        public event Action OnSceneChanged;

        public void LoadLocation(SceneDataSo locationSceneDataSo)
        {
            bool useLoadingScreen = locationSceneDataSo.ShowLoadingScreen || locationSceneDataSo.WaitForKey;

            loadingSceneCoroutine = useLoadingScreen ?
                StartCoroutine(LoadLocationCoroutineWithLoadingScreen(locationSceneDataSo)):
                StartCoroutine(LoadLocationCoroutine(locationSceneDataSo));
        }

        public void OpenScenes()
        {
            foreach(AsyncOperation operation in asyncOperations)
            {
                operation.allowSceneActivation = true;
            }

            IsLoading = false;

            loadingViewController.CloseLoadingView();

            OnSceneChanged?.Invoke();
        }

        private IEnumerator LoadLocationCoroutine(SceneDataSo locationSceneDataSo)
        {
            IsLoading = true;

            List<string> locationsToUnload = new List<string>();

            OnStartSceneLoaded?.Invoke();

            if (CurrentLocationSceneDataSo != null)
            {
                if (!locationSceneDataSo.RequireSceneNames.Contains(CurrentLocationSceneDataSo.SceneName))
                {
                    locationsToUnload.Add(CurrentLocationSceneDataSo.SceneName);
                }

                foreach (string relatedSceneName in CurrentLocationSceneDataSo.RequireSceneNames)
                {
                    if (locationSceneDataSo.RequireSceneNames.Contains(relatedSceneName)) continue;

                    locationsToUnload.Add(relatedSceneName);
                }
            }

            foreach (string locationToUnload in locationsToUnload)
            {
                yield return SceneManager.UnloadSceneAsync(locationToUnload);
                loadedLocationRelatedScenes.Remove(locationToUnload);
            }

            foreach (string relatedSceneName in locationSceneDataSo.RequireSceneNames)
            {
                if (loadedLocationRelatedScenes.Contains(relatedSceneName)) continue;

                yield return SceneManager.LoadSceneAsync(relatedSceneName, LoadSceneMode.Additive);

                loadedLocationRelatedScenes.Add(relatedSceneName);
            }

            if (!loadedLocationRelatedScenes.Contains(locationSceneDataSo.SceneName))
            {
                yield return SceneManager.LoadSceneAsync(locationSceneDataSo.SceneName, LoadSceneMode.Additive);

                loadedLocationRelatedScenes.Add(locationSceneDataSo.SceneName);
            }

            SceneDataSo previousData = CurrentLocationSceneDataSo;

            CurrentLocationSceneDataSo = locationSceneDataSo;

            OnSceneLoaded?.Invoke();

            loadingSceneCoroutine = null;
        }
        private IEnumerator LoadLocationCoroutineWithLoadingScreen(SceneDataSo locationSceneDataSo)
        {
            IsLoading = true;

            asyncOperations = new Queue<AsyncOperation>();

            List<string> locationsToUnload = new List<string>();

            loadingViewController.OpenLoadingView();

            yield return new WaitUntil(loadingViewController.IsShowPresentationComplete);

            OnStartSceneLoaded?.Invoke();

            if (CurrentLocationSceneDataSo != null)
            {
                if (!locationSceneDataSo.RequireSceneNames.Contains(CurrentLocationSceneDataSo.SceneName))
                {
                    locationsToUnload.Add(CurrentLocationSceneDataSo.SceneName);
                }

                foreach (string relatedSceneName in CurrentLocationSceneDataSo.RequireSceneNames)
                {
                    if (locationSceneDataSo.RequireSceneNames.Contains(relatedSceneName)) continue;

                    locationsToUnload.Add(relatedSceneName);
                }
            }

            foreach (string locationToUnload in locationsToUnload)
            {
                yield return SceneManager.UnloadSceneAsync(locationToUnload);
                loadedLocationRelatedScenes.Remove(locationToUnload);
            }

            foreach (string relatedSceneName in locationSceneDataSo.RequireSceneNames)
            {
                if (loadedLocationRelatedScenes.Contains(relatedSceneName)) continue;

                var operation = SceneManager.LoadSceneAsync(relatedSceneName, LoadSceneMode.Additive);

                operation.allowSceneActivation = !locationSceneDataSo.WaitForKey;

                asyncOperations.Enqueue(operation);

                yield return operation.progress >= 0.9f;

                loadedLocationRelatedScenes.Add(relatedSceneName);
            }

            if (!loadedLocationRelatedScenes.Contains(locationSceneDataSo.SceneName))
            {
                var operation = SceneManager.LoadSceneAsync(locationSceneDataSo.SceneName, LoadSceneMode.Additive);

                operation.allowSceneActivation = !locationSceneDataSo.WaitForKey;

                asyncOperations.Enqueue(operation);

                yield return operation.progress >= 0.9f;

                loadedLocationRelatedScenes.Add(locationSceneDataSo.SceneName);
            }

            SceneDataSo previousData = CurrentLocationSceneDataSo;

            CurrentLocationSceneDataSo = locationSceneDataSo;

            loadingViewController.OnLoadingCompleted(locationSceneDataSo.WaitForKey);

            OnSceneLoaded?.Invoke();

            loadingSceneCoroutine = null;

            if (locationSceneDataSo.WaitForKey) yield break;

            yield return new WaitForSeconds(1f);

            OpenScenes();
        }
    }
}