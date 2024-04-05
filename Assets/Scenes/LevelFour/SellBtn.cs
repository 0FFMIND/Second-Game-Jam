using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBtn : MonoBehaviour
{
    public void SellOK()
    {
        CursorManager.isSellOpen = !CursorManager.isSellOpen;
    }
    public void GhostOK()
    {
        CursorManager.isGhostOpen = true;
    }
}
