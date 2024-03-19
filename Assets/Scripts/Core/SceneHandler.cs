using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    private string loadingSceneName = "LoadingScene";

    private string previousSceneName;
    public string PreviousSceneName
    {
        get => previousSceneName;
        set
        {
            if (previousSceneName != value)
            {
                previousSceneName = value;
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
                StartCoroutine(LoadScene());
            }
        }
    }

    AsyncOperation async;

    public System.Action onLoadingSceneCover;

    public System.Action onLoadingSceneUnCover;

    //protected override void OnPreInitialize()
    //{
    //    base.OnPreInitialize();

    //    onLoadingSceneCover += StartNextSceneLoading;
    //    onLoadingSceneUnCover += EndNextSceneLoading;
    //}

    private void Start()
    {
        PresentSceneName = SceneManager.GetActiveScene().name;
    }

    private void StartNextSceneLoading()
    {
        PreviousSceneName = PresentSceneName;
        PresentSceneName = NextSceneName;
        nextSceneName = null;

        //GameManager.Inst.Init();

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
