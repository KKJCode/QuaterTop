using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject singleConUi;
    public GameObject HudUi;
    

    public void SetActiveOnUI()
    {
        exitPanel.SetActive(true);
        singleConUi.SetActive(false);
        HudUi.SetActive(false);
        Time.timeScale = 0;
    }
    public void SetActiveOffUI()
    {
        exitPanel.SetActive(false);
        singleConUi.SetActive(true);
        HudUi.SetActive(true);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
