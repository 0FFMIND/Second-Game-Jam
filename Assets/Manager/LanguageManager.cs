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
        //To Do: 把savesystem初始化写进来
        //在一开始把txt存进gameDict
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
                    line.Trim();//去除首尾的空格
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
        //当signal变化的时候加载一次
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
        //判断nowOption与preOption的关系
        if (nowOption == LanguageOption.Chinese)
        {
            preOption = LanguageOption.Chinese;
            changeTo = LanguageOption.Chinese;
        } else if (nowOption == LanguageOption.English)
        {
            preOption = LanguageOption.English;
            changeTo = LanguageOption.English;
        }
        //先遍历所有的物体
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
                //English对应前面的key，Chinese对应后面的value
                //当前是English，想要切换Chinese
                if (changeTo == LanguageOption.Chinese)
                {
                    if (gameDict.ContainsKey(singleText))
                    {
                        singleText = gameDict[singleText];

                    }
                }
                //当前是Chinese，想要切换English
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
