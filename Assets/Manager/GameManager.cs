using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TransManager.Instance.ChangeScene("TitleScene");
        }
    }
}
