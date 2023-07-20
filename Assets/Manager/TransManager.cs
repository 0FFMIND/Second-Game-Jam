using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransManager : Singleton<TransManager>
{
    public void ChangeScene(string sceneName)
    {
        Transitioner.Instance.TransitionToScene(sceneName);
    }
}
