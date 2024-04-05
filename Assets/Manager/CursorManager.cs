using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CursorManager : Singleton<CursorManager>
{
    public static bool isSellOpen = false;
    public static bool isGhostOpen = false;
    public Vector2 cursorPos;
    private Vector3 pos = new Vector3(-100,-100,0);
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
    public void SetPos(Vector3 pos)
    {
        this.pos = pos;
    }
    public Vector3 GetPos()
    {
        return pos;
    }
}
