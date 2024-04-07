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
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "修建一座污水治理器并成功治理污水";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "修建一台气候调节器调节太阳雨气候";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "抵挡住被雨天吸引的老鼠";
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
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "放置污水收集器清空场上的污水";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "放置两台气候调节器调整当局的雨雾天气候";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "抵挡住被雨天气候吸引的老鼠的攻击";
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
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "放置污水收集器清空场上的污水";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "放置两台气候调节器调整当局的雨雾天气候";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "抵挡住被雨天气候吸引的敌人的攻击，同样保护基地的血量";
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
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "建造两台气候调节器调节雪天气候";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "建立热熔垃圾回收站点回收星际轨道上的垃圾";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "抵挡住想要侵占基地的外来生物，同样保护基地的血量";
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
                    itemList[0].GetComponent<TextMeshProUGUI>().text = "建造三台气候调节器调节雪天气候";
                    itemList[1].GetComponent<TextMeshProUGUI>().text = "建立热熔垃圾回收站点回收星际轨道上的垃圾";
                    itemList[2].GetComponent<TextMeshProUGUI>().text = "抵挡住想要侵占基地的外来生物，同样保护基地的血量";
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
