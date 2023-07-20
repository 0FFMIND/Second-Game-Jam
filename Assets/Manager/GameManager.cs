using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TransManager.Instance.ChangeScene("TitleScene");
        }
    }
}
