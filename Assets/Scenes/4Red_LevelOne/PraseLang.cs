using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
public class PraseLang : MonoBehaviour
{
    public TextMeshProUGUI quest;
    public TextMeshProUGUI power;
    public TextMeshProUGUI weather;
    public TextMeshProUGUI now;
    public void Start()
    {
        if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
        {
            quest.text = "Quests List";
            power.text = "Power";
            weather.text = "Weather";
            if (SceneManager.GetActiveScene().name == "LevelOne" || SceneManager.GetActiveScene().name == "LevelTwo")
            {
                now.text = "SunShower";
            }
            else if (SceneManager.GetActiveScene().name == "LevelThree")
            {
                now.text = "Foggy";
            }
            else if (SceneManager.GetActiveScene().name == "LevelFour" || SceneManager.GetActiveScene().name == "LevelFive" || SceneManager.GetActiveScene().name == "LevelSix")
            {
                now.text = "Snowy";
            }
           
        }
        else if(LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
        {
            quest.text = "���������б�";
            power.text = "��������";
            weather.text = "��ǰ����";
            if (SceneManager.GetActiveScene().name == "LevelOne" || SceneManager.GetActiveScene().name == "LevelTwo")
            {
                now.text = "̫����";
            }
            else if (SceneManager.GetActiveScene().name == "LevelThree")
            {
                now.text = "������";
            }
            else if (SceneManager.GetActiveScene().name == "LevelFour" || SceneManager.GetActiveScene().name == "LevelFive" || SceneManager.GetActiveScene().name == "LevelSix")
            {
                now.text = "��ѩ��";
            }
        }
    }

}
