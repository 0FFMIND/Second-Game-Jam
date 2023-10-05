using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject uiPanel;
    public Text sInfo;
    [SerializeField] private GameState gameState;
    //当点击时执行
    private void Start()
    {
        gameState = GameState.normal;
        uiPanel.SetActive(true);
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    private void UpdateUI()
    {
        switch (gameState)
        {
            case GameState.slow:
                sInfo.text = "0.5x";
                break;
            case GameState.normal:
                sInfo.text = "1x";
                break;
            case GameState.fast:
                sInfo.text = "2x";
                break;
            default: break;
        }
    }
    private void EnableUI()
    {
        switch (gameState)
        {
            case GameState.slow:
                Time.timeScale = 0.5f;
                break;
            case GameState.normal:
                Time.timeScale = 1f;
                break;
            case GameState.fast:
                Time.timeScale = 2f;
                break;
            default: break;
        }
    }
    public void PauseGame()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
        UpdateUI();
        Time.timeScale = 0.0f;
        pauseButton.SetActive(false);
        uiPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ResumeGameNormal()
    {
        gameState = GameState.normal;
        UpdateUI();
    }
    public void ResumeGameSlow()
    {
        gameState = GameState.slow;
        UpdateUI();
    }
    public void ResumeGameFast()
    {
        gameState = GameState.fast;
        UpdateUI();

    }
    public void BackToOpenScene()
    {
        Time.timeScale = 1f;
        TransManager.Instance.ChangeScene("OpenScene");
    }
    public void ResumeGameBtn()
    {
        EnableUI();
        uiPanel.SetActive(true);
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void MouseEnterSFX()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
}
