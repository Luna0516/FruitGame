using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    private const string loadingSceneName = "GameLoadingScene";

    private string previousSceneName;
    public string PreviousSceneName
    {
        get => previousSceneName;
        set
        {
            if (previousSceneName != value)
            {
                previousSceneName = value;
                Debug.Log($"이전 씬 이름 : {previousSceneName}");
            }
        }
    }

    private string presentSceneName;
    public string PresentSceneName
    {
        get => presentSceneName;
        set
        {
            if (presentSceneName != value)
            {
                presentSceneName = value;
                Debug.Log($"현재 설정된 씬 이름 : {presentSceneName}");
            }
        }
    }

    private string nextSceneName;
    public string NextSceneName
    {
        get => nextSceneName;
        set
        {
            if(nextSceneName != value)
            {
                nextSceneName = value;
                Debug.Log($"다음 씬 이름 : {nextSceneName}");
                StartCoroutine(LoadScene());
            }
        }
    }

    AsyncOperation async;

    public System.Action onLoadingSceneCover;
    public System.Action onLoadingSceneUnCover;

    protected override void OnAwake()
    {
        onLoadingSceneCover += StartNextSceneLoading;
        onLoadingSceneUnCover += EndNextSceneLoading;

        PresentSceneName = SceneManager.GetActiveScene().name;
    }

    private void StartNextSceneLoading()
    {
        PreviousSceneName = PresentSceneName;
        PresentSceneName = NextSceneName;
        nextSceneName = null;

        async.allowSceneActivation = true;

        SceneManager.UnloadSceneAsync(PreviousSceneName);
    }

    private void EndNextSceneLoading()
    {
        SceneManager.UnloadSceneAsync(loadingSceneName);
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);

        loadingScene.allowSceneActivation = false;

        while (loadingScene.progress < 0.9f)
        {
            yield return null;
        }

        loadingScene.allowSceneActivation = true;

        StartCoroutine(LoadNextScene(NextSceneName));
    }

    private IEnumerator LoadNextScene(string nextSceneName)
    {
        async = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }
    }
}
