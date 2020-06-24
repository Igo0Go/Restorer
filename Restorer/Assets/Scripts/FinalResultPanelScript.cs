using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalResultPanelScript : MonoBehaviour
{
    public List<Text> resultTexts;


    void Start()
    {
        resultTexts[0].text = "Сильный стиль: " + GameData.currentElementsOnStyle[DanceStyle.Strong] + "/" + GameData.maxElementsOnStyle[DanceStyle.Strong];
        resultTexts[1].text = "Средний стиль: " + GameData.currentElementsOnStyle[DanceStyle.Middle] + "/" + GameData.maxElementsOnStyle[DanceStyle.Middle];
        resultTexts[2].text = "Быстрый стиль: " + GameData.currentElementsOnStyle[DanceStyle.Fast] + "/" + GameData.maxElementsOnStyle[DanceStyle.Fast];

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadScene(int index) => SceneManager.LoadScene(index);
    public void Exit() => Application.Quit();
}

public static class GameData
{
    public static Dictionary<DanceStyle, int> maxElementsOnStyle;
    public static Dictionary<DanceStyle, int> currentElementsOnStyle;

    public static void Init()
    {
        maxElementsOnStyle = new Dictionary<DanceStyle, int>();
        currentElementsOnStyle = new Dictionary<DanceStyle, int>();

        maxElementsOnStyle.Add(DanceStyle.Strong, 0);
        maxElementsOnStyle.Add(DanceStyle.Middle, 0);
        maxElementsOnStyle.Add(DanceStyle.Fast, 0);
        
        currentElementsOnStyle.Add(DanceStyle.Strong, 0);
        currentElementsOnStyle.Add(DanceStyle.Middle, 0);
        currentElementsOnStyle.Add(DanceStyle.Fast, 0);
    }

    public static void AddInMax(DanceStyle style)
    {
        maxElementsOnStyle[style]++;
    }
    public static void AddInCurrent(DanceStyle style)
    {
        currentElementsOnStyle[style]++;
    }
}
