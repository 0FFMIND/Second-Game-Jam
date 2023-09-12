using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeveloperMenu : TitleMenuBase<DeveloperMenu>
{
    public override void OnBackPressed()
    {
        gameObject.GetComponentInParent<TitleController>().mainBackground.SetActive(true);
        gameObject.GetComponentInParent<TitleController>().subBackground.SetActive(false);
        Close();
    }
}
