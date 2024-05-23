using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    private bool isPause = false;
    public void Init()
    {
        EventManager.Instance.AddEventListener("OnESCDown", HandleESCDown);
    }

    private void HandleESCDown()
    {
        if (SceneManager.GetActiveScene().name == "OpenScene" || SceneManager.GetActiveScene().name == "IntroScene")
        {
            TransManager.Instance.ChangeScene("TitleScene");
        }
        if (SceneManager.GetActiveScene().name == "LevelOne" || SceneManager.GetActiveScene().name == "LevelTwo" || SceneManager.GetActiveScene().name == "LevelThree" || SceneManager.GetActiveScene().name == "LevelFour" || SceneManager.GetActiveScene().name == "LevelFive" || SceneManager.GetActiveScene().name == "LevelSix")
        {
            isPause = !isPause;
            if (!isPause)
            {
                PauseButton.Instance.PauseGame();
            }
            else if (isPause)
            {
                PauseButton.Instance.ResumeGameBtn();

            }
        }
    }
}
