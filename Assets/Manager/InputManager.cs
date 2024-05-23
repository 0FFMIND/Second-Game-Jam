
using UnityEngine;
public class InputManager : SingletonMono<InputManager>
{
    public static bool isSellOpen = false;
    public static bool isGhostOpen = false;
    public Vector2 cursorPos;
    private Vector3 pos = new Vector3(-100,-100,0);
    public void Init()
    {

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
    public void SetPos(Vector3 pos)
    {
        this.pos = pos;
    }
    public Vector3 GetPos()
    {
        return pos;
    }
}
