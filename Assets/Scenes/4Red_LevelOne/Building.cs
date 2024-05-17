using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    public HealthSystem healthSystem;
    GameObject[] sellPanel;
    GameObject[] sellCanvas;
    private bool isOpen;
    private void Start()
    {
        isOpen = true;
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void Update()
    {
        sellPanel = GameObject.FindGameObjectsWithTag("sell");
        sellCanvas = GameObject.FindGameObjectsWithTag("sellCanvas");
        if ((Input.GetMouseButtonDown(1) || !InputManager.isSellOpen) && isOpen)
        {
            isOpen = !isOpen;
            for (int i = 0; i < sellPanel.Length; i++)
            {
                sellPanel[i].SetActive(false);
            }
        }
        else if((Input.GetMouseButtonDown(1) || InputManager.isSellOpen) && !isOpen)
        {
            isOpen = !isOpen;
            for (int i = 0; i < sellCanvas.Length; i++)
            {
                for (int x = 0; x < sellCanvas[i].gameObject.transform.childCount; x++)
                {
                    sellCanvas[i].gameObject.transform.GetChild(x).gameObject.SetActive(true);
                }
            }
        }
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
}
