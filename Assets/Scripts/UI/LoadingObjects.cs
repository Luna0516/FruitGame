using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingObjects : MonoBehaviour
{
    private GameObject[,] loadingCubes;

    private float waitTime = 0.01f;

    private WaitForSeconds wait;

    bool isLoading = false;

    int widthCubeCount;
    int heightCubeCount;

    int yCount;

    private void Awake()
    {
        heightCubeCount = transform.childCount;
        widthCubeCount = transform.GetChild(0).childCount;

        loadingCubes = new GameObject[heightCubeCount, widthCubeCount];

        for(int i = 0; i < heightCubeCount; i++)
        {
            Transform child = transform.GetChild(i);

            for(int j = 0; j < widthCubeCount; j++)
            {
                loadingCubes[i, j] = child.GetChild(j).gameObject;
                loadingCubes[i, j].SetActive(false);
            }
        }

        wait = new WaitForSeconds(waitTime);
    }

    private void Start()
    {
        StartCoroutine(ActiveLoadingLine(true));
    }

    private IEnumerator ActiveLoadingLine(bool active)
    {
        for (int i = 0; i < heightCubeCount; i++)
        {
            StartCoroutine(ActiveLoadingCube(i, active));
            yield return wait;
        }
    }

    private IEnumerator ActiveLoadingCube(int _y, bool active)
    {
        for(int i = 0; i < widthCubeCount; i++)
        {
            loadingCubes[_y, i].SetActive(active);

            yield return wait;
        }

        yCount++;

        if (yCount == heightCubeCount)
        {
            yCount = 0;
            isLoading = active;

            if (isLoading)
            {
                SceneHandler.Inst.onLoadingSceneCover?.Invoke();
                StartCoroutine(ActiveLoadingLine(false));
            }
            else
            {
                SceneHandler.Inst.onLoadingSceneUnCover?.Invoke();
            }
        }
    }
}
