using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public ButtonType type;

    private void Awake()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(ClickButton);
    }

    private void ClickButton()
    {
        switch (type)
        {
            case ButtonType.GameStart:
                GameStart();
                break;
            case ButtonType.GameRule:
                GameRule();
                break;
            case ButtonType.GameQuit:
                GameQuit();
                break;
            case ButtonType.None:
            default:
                break;
        }
    }

    private void GameStart()
    {
        Debug.Log("게임 시작 버튼 누름");
        SceneHandler.Inst.NextSceneName = "GamePlayScene";
    }

    private void GameRule()
    {
        Debug.Log("게임 방법 버튼 누름");
    }

    private void GameQuit()
    {
        Debug.Log("게임 종료 버튼 누름");
    }
}
