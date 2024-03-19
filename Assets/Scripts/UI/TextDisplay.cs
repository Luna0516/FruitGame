using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    private float blinkSpeed = 1f;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(BlinkTexture());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneHandler.Inst.NextSceneName = "GameStartScene";
        }
    }

    private IEnumerator BlinkTexture()
    {
        while (true)
        {
            Color color = text.color;

            while (color.a > 0)
            {
                color.a -= blinkSpeed * Time.deltaTime;
                text.color = color;
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);

            while (color.a < 1)
            {
                color.a += blinkSpeed * Time.deltaTime;
                text.color = color;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
