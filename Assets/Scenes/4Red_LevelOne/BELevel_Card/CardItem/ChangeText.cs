using UnityEngine.UI;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    string text;
    private void Start()
    {
        text = this.GetComponent<Text>().text;
        SaveManager.Instance.LoadLevel();
        if(SaveManager.Instance.IsAngel == 2)
        {
            if(LanguageManager.Instance.CurrentLanguage == LanguageOption.English)
            {
                text = "Aggressive";
            }else if(LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
            {
                text = "¼¤½ø";
            }
        }
        if(SaveManager.Instance.IsAngel == 1)
        {
            if (LanguageManager.Instance.CurrentLanguage == LanguageOption.English)
            {
                text = "Gentle";
            }
            else if (LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
            {
                text = "ÎÂºÍ";
            }
        }
    }
}
