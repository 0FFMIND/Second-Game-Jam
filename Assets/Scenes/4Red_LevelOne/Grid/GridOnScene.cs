using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOnScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridXY gridXY = new GridXY(11, 5, 1.4f,transform.position);
    }

}
