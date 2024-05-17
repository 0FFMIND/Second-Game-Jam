using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum LanguageOption
{
    English,
    Chinese
}
public class LanguageManager : SingletonMono<LanguageManager>
{
    // ���﹫�г���Debug��
    public LanguageOption currentLanguage = LanguageOption.Chinese;
    public LanguageOption previousLanguage = LanguageOption.Chinese;
    public bool hasLanguageChanged;
    // ˽�б���
    private readonly Dictionary<string, string> gameDictionary = new Dictionary<string, string>();
    private string previousScene = "TitleScene";
    private void Awake()
    {
        LoadLanguageFile("Language");
    }
    private void Update()
    {
        // ����������˽���ͷ���������Կ�ı�
        if (currentLanguage != previousLanguage)
        {
            SwitchLanguage(currentLanguage);
        }
        ChangeLanguage();
        // �ǿ�ͷ����ʱ����ΪĬ����������Ϊ���ģ����Ĭ��������Ӣ�ģ�ÿһ�α䳡��ʱ����ز�ͬ���Կ�
        if(SceneManager.GetActiveScene().name != previousScene)
        {
            SwitchLanguage(currentLanguage);
            previousScene = SceneManager.GetActiveScene().name;
        }
    }
    private void ChangeLanguage()
    {
        if (hasLanguageChanged)
        {
            LanguageOption option = previousLanguage;
            currentLanguage = (LanguageOption)(((int)option + 1) % 2);
            hasLanguageChanged = false;
        }
    }
    private void LoadLanguageFile(string path)
    {
        TextAsset targetAsset = Resources.Load<TextAsset>(path);
        string[] lines = targetAsset.text.Split('\n');
        foreach (string line in lines)
        {
            string[] lineParts = line.Split(':');
            if (lineParts.Length < 2) continue;
            gameDictionary.Add(lineParts[0].Trim(), lineParts[1].Trim());
        }
    }
    private void GetAllObjectsInScene(Transform parent,ref List<GameObject> objects)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            objects.Add(child.gameObject);
            if (child.childCount > 0)
            {
                //�򵥵ĵݹ�
                GetAllObjectsInScene(child, ref objects);
            }
        }
    }
    public void SwitchLanguage(LanguageOption targetLanguage)
    {
        previousLanguage = currentLanguage;
        currentLanguage = targetLanguage;
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "TitleScene")
        {
            GameObject.Find("TitleCanvas").gameObject.GetComponent<TitleController>().hasLangChangeInTitle = true;
        }
        List<GameObject> allObjects = new List<GameObject>(scene.GetRootGameObjects());
        List<GameObject> childObjects = new List<GameObject>();
        foreach(GameObject obj in allObjects)
        {
            childObjects.Add(obj);
            GetAllObjectsInScene(obj.transform, ref childObjects);
        }
        foreach(GameObject obj in childObjects)
        {
            if(obj.GetComponent<Text>() != null)
            {
                obj.GetComponent<Text>().text = UpdateLanguageText(targetLanguage, obj.GetComponent<Text>().text);
            }
            if(obj.GetComponent<TextMeshProUGUI>() != null)
            {
                obj.GetComponent<TextMeshProUGUI>().text = UpdateLanguageText(targetLanguage, obj.GetComponent<TextMeshProUGUI>().text);
            }
        }
    }
    private string UpdateLanguageText(LanguageOption targetLanguage, string text)
    {
        if (targetLanguage == LanguageOption.Chinese && gameDictionary.ContainsKey(text))
        {
            return gameDictionary[text];
        }
        else if (targetLanguage == LanguageOption.English)
        {
            string firstKey = gameDictionary.FirstOrDefault(pair => pair.Value == text).Key;
            if (firstKey != null)
            {
                return firstKey;
            }
            else return text;
        }
        else return text;
    }
}
