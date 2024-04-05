using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBtnController : MonoBehaviour
{
    public OpenPackage openPackage;
    public GameObject confirmBtn;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if(openPackage.cardTempStore.Count != 0)
            {
                confirmBtn.SetActive(true);
            }
            
        });
    }
}
