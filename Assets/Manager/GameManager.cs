using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name =="OpenScene")
        {
            TransManager.Instance.ChangeScene("TitleScene");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name == "LevelOne" || SceneManager.GetActiveScene().name == "LevelTwo" || SceneManager.GetActiveScene().name == "LevelThree" || SceneManager.GetActiveScene().name == "LevelFour" || SceneManager.GetActiveScene().name == "LevelFive" || SceneManager.GetActiveScene().name == "LevelSix"))
        {
            if (PauseButton.Instance.isPause)
            {
                PauseButton.Instance.ResumeGameBtn();
            }

            else
            {
                StartCoroutine(Pause());
            } 
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.1f);
        PauseButton.Instance.PauseGame();
    }
}
