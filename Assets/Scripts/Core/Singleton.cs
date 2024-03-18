using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool isShutDown = false;

    private static object lockObject = new object();

    private static T instance;
    public static T Inst
    {
        get
        {
            if (isShutDown) 
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                return null; 
            }

            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject($"{typeof(T).Name} (Singleton)");
                        instance = singletonObject.AddComponent<T>();

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }

    private void Awake()
    {
        lock (lockObject)
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
                OnAwake();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnAwake() { }

    private void OnApplicationQuit()
    {
        isShutDown = true;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            isShutDown = true;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isShutDown)
        {
            if (mode != LoadSceneMode.Additive)
            {
                Initialize();
            }
        }
    }

    protected virtual void Initialize() { }
}