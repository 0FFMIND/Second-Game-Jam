using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public enum LanguageOption
{
    English,
    Chinese,
}
public class LanguageManager : Singleton<LanguageManager>
{
    public LanguageOption nowOption = LanguageOption.English;
    public LanguageOption preOption = LanguageOption.English;
    public bool setChange;
    Dictionary<string, string> gameDict = new Dictionary<string, string>();
    private void Start()
    {
        //To Do: ��savesystem��ʼ��д����
        //��һ��ʼ��txt���gameDict
        string path = "Language";
        TextAsset targetAsset = Resources.Load<TextAsset>(path);
        string[] lines = targetAsset.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                continue;
            }
            else
            {
                string[] nowLines = lines[i].Split(':');
                foreach (string line in nowLines)
                {
                    line.Trim();//ȥ����β�Ŀո�
                }
                gameDict.Add(nowLines[0], nowLines[1]);
            }
        }
    }
    private void Update()
    {
        if(nowOption != preOption)
        {
            SwitchLanguage(nowOption);
        }
        //��signal�仯��ʱ�����һ��
        if (setChange)
        {
            setChange = false;
            SwitchLanguage(nowOption);
        }
    }
    private void FindAll(Transform parent, ref List<GameObject> newObj)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform temp = parent.GetChild(i);
            newObj.Add(temp.gameObject);
            if(temp.childCount > 0)
            {
                FindAll(temp, ref newObj);
            }
        }
    }
    public void SwitchLanguage(LanguageOption changeTo)
    {
        //�ж�nowOption��preOption�Ĺ�ϵ
        if (nowOption == LanguageOption.Chinese)
        {
            preOption = LanguageOption.Chinese;
            changeTo = LanguageOption.Chinese;
        } else if (nowOption == LanguageOption.English)
        {
            preOption = LanguageOption.English;
            changeTo = LanguageOption.English;
        }
        //�ȱ������е�����
        Scene scene = SceneManager.GetActiveScene();
        List<GameObject> allObj = new List<GameObject>(scene.GetRootGameObjects());
        List<GameObject> newObj = new List<GameObject>();
        foreach (GameObject singleObj in allObj)
        {
            newObj.Add(singleObj);
            FindAll(singleObj.transform, ref newObj);
        }
        foreach (GameObject singleObj in newObj)
        {
            string singleText = null;
            bool isTMP = false;
            if (singleObj.GetComponent<TMP_Text>() != null && singleObj.GetComponent<TMP_Text>().text != null)
            {
                singleText = singleObj.GetComponent<TMP_Text>().text;
                isTMP = true;
            }
            else if (singleObj.GetComponent<Text>() != null && singleObj.GetComponent<Text>().text != null)
            {
                singleText = singleObj.GetComponent<Text>().text;
                isTMP = false;
            }
            if(singleText != null)
            {
                //English��Ӧǰ���key��Chinese��Ӧ�����value
                //��ǰ��English����Ҫ�л�Chinese
                if (changeTo == LanguageOption.Chinese)
                {
                    if (gameDict.ContainsKey(singleText))
                    {
                        singleText = gameDict[singleText];

                    }
                }
                //��ǰ��Chinese����Ҫ�л�English
                else if (changeTo == LanguageOption.English)
                {
                    string firstKey = gameDict.FirstOrDefault(pair => pair.Value == singleText).Key;
                    if(firstKey != null)
                    {
                        singleText = firstKey;

                    }
                }
                if (isTMP)
                {
                    singleObj.GetComponent<TMP_Text>().text = singleText;
                }
                else
                {
                    singleObj.GetComponent<Text>().text = singleText;
                }
            }
        }
        if (nowOption == LanguageOption.English) preOption = LanguageOption.English;
        else if (nowOption == LanguageOption.Chinese) preOption = LanguageOption.Chinese;
    }
}
