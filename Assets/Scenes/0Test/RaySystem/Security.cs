using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    //自身能够360度旋转
    private void Update()
    {
        SelfRotation();
    }
    private void SelfRotation()
    {

    }
}
