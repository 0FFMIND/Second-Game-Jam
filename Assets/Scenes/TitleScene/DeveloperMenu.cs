using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeveloperMenu : TitleMenuBase<DeveloperMenu>
{
    public override void OnBackPressed()
    {
        Close();
    }
}
