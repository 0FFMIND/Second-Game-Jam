using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    slow,
    normal,
    fast,
}
public class PauseButton : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pausePanel;
    //当点击时执行
    private void Start()
    {
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void ResumeGameSlow()
    {
        Time.timeScale = 0.5f;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void ResumeGameFast()
    {
        Time.timeScale = 2f;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void BackToOpenScene()
    {
        Time.timeScale = 1f;
        TransManager.Instance.ChangeScene("OpenScene");
    }
}
