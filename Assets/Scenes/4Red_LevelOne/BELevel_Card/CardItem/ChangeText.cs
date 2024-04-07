using UnityEngine.UI;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    private void Start()
    {
        SaveManager.Instance.LoadLevel();
        if(SaveManager.Instance.IsAngel == 2)
        {
            if(LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                GetComponent<Text>().text = "Aggressive";
            }else if(LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                GetComponent<Text>().text = "����";
            }
        }
        if(SaveManager.Instance.IsAngel == 1)
        {
            if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                GetComponent<Text>().text = "Gentle";
            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                GetComponent<Text>().text = "�º�";
            }
        }
    }
}
