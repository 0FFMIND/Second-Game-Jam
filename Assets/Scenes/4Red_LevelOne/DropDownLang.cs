using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropDownLang : MonoBehaviour
{
    Dropdown dropdown;
    GameObject[] itemList;
    private void Start()
    {
        dropdown = gameObject.GetComponent<Dropdown>();
    }
    private void Update()
    {
        SwitchLanguage();
    }
    private void SwitchLanguage()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "LevelOne")
        {
            if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "�޽�һ����ˮ���������ɹ�������ˮ";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "�޽�һ̨�������������̫��������";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "�ֵ�ס����������������";
                }

            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "Construct a treatment plant to remove sewage";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "Build climate regulator to regulate climate";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "Resist rats attracted by rainy days";
                }
            }
        }
        if(scene.name == "LevelTwo")
        {
            if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "������ˮ�ռ�����ճ��ϵ���ˮ";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "������̨����������������ֵ�����������";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "�ֵ�ס��������������������Ĺ���";
                }

            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "Place sewage collector to clear all the sewage on the field.";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "Place two climate regulators to adjust the current rainy and foggy weather.";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "Defend against the attacks of enemies.";
                }
            }
        }
        if (scene.name == "LevelThree")
        {
            if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "������ˮ�ռ�����ճ��ϵ���ˮ";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "������̨����������������ֵ�����������";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "�ֵ�ס���������������ĵ��˵Ĺ�����ͬ���������ص�Ѫ��";
                }

            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "Place sewage collector to clear all the sewage on the field.";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "Place two climate regulators to adjust the current rainy and foggy weather.";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "Defend against the attacks of enemies, and protect your base.";
                }
            }
        }
        if (scene.name == "LevelFour")
        {
            if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "������̨�������������ѩ������";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "����������������վ������Ǽʹ���ϵ�����";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "�ֵ�ס��Ҫ��ռ���ص��������ͬ���������ص�Ѫ��";
                }

            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "Build two climate regulators to adjust the snowy weather conditions.";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "Establish a thermal waste recycling station to recycle the garbage in the interstellar orbit.";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "Defend against the alien creatures that want to invade the base, and protect your base.";
                }
            }
        }
        if (scene.name == "LevelFive")
        {
            if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "������̨�������������ѩ������";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "����������������վ������Ǽʹ���ϵ�����";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "�ֵ�ס��Ҫ��ռ���ص��������ͬ���������ص�Ѫ��";
                }

            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "Build three climate regulators to adjust the snowy weather conditions.";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "Establish a thermal waste recycling station to recycle the garbage in the interstellar orbit.";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "Defend against the alien creatures that want to invade the base, and protect your base.";
                }
            }
        }
    }

}
