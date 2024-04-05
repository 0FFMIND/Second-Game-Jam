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
        SaveManager.Instance.LoadSettings();
        if (!SaveManager.Instance.IsEnglishLanguage)
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
        else if(SaveManager.Instance.IsEnglishLanguage){
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
