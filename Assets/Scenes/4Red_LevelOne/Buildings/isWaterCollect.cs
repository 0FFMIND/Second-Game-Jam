using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isWaterCollect : MonoBehaviour
{
    private Vector3 position;
    private bool isWaterCollected = false;
    public float Timer;
    private void Start()
    {
        position = this.transform.position;
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > 48f && !isWaterCollected)
        {
            isWaterCollected = true;
            //InputManager.Instance.SetPos(this.transform.position);
        }
    }
}
