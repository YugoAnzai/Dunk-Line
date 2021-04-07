using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    private class LoadingMonobehaviour : MonoBehaviour { }

    [System.Serializable]
    public enum Scene
    {

        MainScene = 0,

        LoadingScene = 1,

        MenuScene = 2,
        EndGameScene = 3,

        Level00 = 10,
        Level01 = 11,
        Level02 = 12,
        Level03 = 13,
        Level04 = 14,
        Level05 = 15,
        Level06 = 16,
        Level07 = 17,
        Level08 = 18,
        Level09 = 19,
        Level10 = 20,

    }

    private static Action OnLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene scene)
    {
        OnLoaderCallback = () =>
        {
            GameObject loadinGameObject = new GameObject("Loading Game Object");
            loadinGameObject.AddComponent<LoadingMonobehaviour>().StartCoroutine(LoadSceneAsync(scene));
            LoadSceneAsync(scene);
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoadAdditive(Scene scene)
    {

        // Check if scene is already laoded
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            UnityEngine.SceneManagement.Scene unityScene = SceneManager.GetSceneAt(i);
            if (unityScene.name == scene.ToString()) {
                return;
            }
        }

        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }

    public static void UnloadAdditive(Scene scene) {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1;
        }
    }

    public static void LoaderCallback()
    {
        OnLoaderCallback?.Invoke();
        OnLoaderCallback = null;
    }

    public static Scene GetThisScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return (Scene)System.Enum.Parse(typeof(Scene), sceneName);
    }

    public static void ReloadThisScene()
    {
        Scene curScene = GetThisScene();
        Load(curScene);
    }

}