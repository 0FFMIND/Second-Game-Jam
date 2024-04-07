using UnityEngine;
using UnityEngine.UI;
public class TextChange : MonoBehaviour
{
    public int num;
    public Text text;
    void Start()
    {
        SaveManager.Instance.LoadLevel();
        num = SaveManager.Instance.IsAngel;
        text = gameObject.GetComponent<Text>();
        if (num == 1 && LanguageManager.Instance.currentLanguage == LanguageOption.Chinese) text.text = "�º�";
        else if (num == 1 && LanguageManager.Instance.currentLanguage == LanguageOption.English) text.text = "Gentle";
        else if (num == 2 && LanguageManager.Instance.currentLanguage == LanguageOption.Chinese) text.text = "����";
        else if (num == 2 && LanguageManager.Instance.currentLanguage == LanguageOption.Chinese) text.text = "Aggressive";
    }

}
