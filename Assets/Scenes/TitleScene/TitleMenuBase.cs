using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TitleMenuBase : MonoBehaviour
{
    public abstract void OnBackPressed();
}
public abstract class TitleMenuBase<T> : TitleMenuBase where T : TitleMenuBase<T>
{
    public void Open()
    {
        gameObject.SetActive(true);
        gameObject.GetComponentInParent<TitleController>().OpenMenu(this);
    }
    protected void Close()
    {
        gameObject.GetComponentInParent<TitleController>().CloseMenu();
    }
}
