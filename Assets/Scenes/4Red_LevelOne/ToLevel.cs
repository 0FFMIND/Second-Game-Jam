using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLevel : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.IntroScene);
    }
    public void Change(string sceneName)
    {
        TransManager.Instance.ChangeScene(sceneName);
    }
}
