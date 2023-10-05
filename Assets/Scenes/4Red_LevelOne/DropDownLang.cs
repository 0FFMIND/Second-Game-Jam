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
            if (LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
            {
                if (GameObject.FindWithTag("ITEM") != null)
                {
                    itemList = GameObject.FindGameObjectsWithTag("ITEM");
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "�޽�һ����ˮ���������ɹ�������ˮ";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "�޽�һ̨�������������̫��������";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "�ֵ�ס����������������";
                }

            }
            else if (LanguageManager.Instance.CurrentLanguage == LanguageOption.English)
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
    }

}
