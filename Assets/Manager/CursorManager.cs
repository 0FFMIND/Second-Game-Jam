using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CursorManager : Singleton<CursorManager>
{
    public Vector2 cursorPos;
    private void Update()
    {
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            cursorPos = Camera.main.ScreenToWorldPoint(touch.position);
        }
#else
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif
        transform.position = new Vector3(cursorPos.x, cursorPos.y, 1f);
    }
}
