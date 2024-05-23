
using UnityEngine;
public class InputManager : SingletonMono<InputManager>
{
    // 先注释掉屎山
    //public static bool isSellOpen = false;
    //public static bool isGhostOpen = false;
    //private Vector3 pos = new Vector3(-100, -100, 0);


    public Vector2 cursorPos;
    
    public void Init()
    {
        // 用来启动InputManager
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventManager.Instance.EventTrigger("OnESCDown");
        }
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Instance.EventTrigger("OnMouseDown");
        }
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPos.x, cursorPos.y, 1f);
    }

    // 忘了这段代码写来干什么的了
    //public void SetPos(Vector3 pos)
    //{
    //    this.pos = pos;
    //}
    //public Vector3 GetPos()
    //{
    //    return pos;
    //}
}
