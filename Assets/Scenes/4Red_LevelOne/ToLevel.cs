using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLevel : MonoBehaviour
{
    public void Change(string sceneName)
    {
        TransManager.Instance.ChangeScene(sceneName);
    }
}
