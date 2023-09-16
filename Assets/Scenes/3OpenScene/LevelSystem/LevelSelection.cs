using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    // ���ص�ÿһ��С������
    [SerializeField] private bool unlocked;
    public Image unlockImage;
    public GameObject[] stars;

    private void UpdateLevelImage() //ȡ����unlocked��������UI����
    {
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
        else if (unlocked)
        {
            unlockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
            }
        }
    }
    public void PressSelection(string _LevelName)
    {
        if (unlocked)
        {
            TransManager.Instance.ChangeScene(_LevelName);
        }
    }
}
