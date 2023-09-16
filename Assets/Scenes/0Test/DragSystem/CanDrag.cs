using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDrag : MonoBehaviour
{
    //CanDrag的东西需要加上Collider2D才能生效(继承了OnMouse)
    [SerializeField] private bool isFinished;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Transform correctTrans;
    private void Start()
    {
        startPos = transform.position;
    }
    private void OnMouseDrag()
    {
        if (!isFinished)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1f);
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
    }
    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - correctTrans.position.x) <= 1f &&
            Mathf.Abs(transform.position.y - correctTrans.position.y) <= 1f){
            transform.position = new Vector3(correctTrans.position.x, correctTrans.position.y, 1f);
            isFinished = true;
        }
        else
        {
            transform.position = new Vector3(startPos.x, startPos.y, 1f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
