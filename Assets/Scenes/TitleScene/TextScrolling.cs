using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrolling : MonoBehaviour
{
    public GameObject firstText;
    public GameObject secondText;
    public float speed;
    private float defaultdeltaPos;
    public float chdeltaPos;
    private float deltaPos;
    public Vector3 chstopPos;
    public Vector3 startPos;
    private Vector3 stopPos;
    private bool canset = true;
    private LanguageOption defalutOption;
    private LanguageOption nowOption;
    void Start()
    {
        startPos = firstText.GetComponent<RectTransform>().anchoredPosition3D;
        stopPos = secondText.GetComponent<RectTransform>().anchoredPosition3D;
        defaultdeltaPos = stopPos.x - startPos.x;
        deltaPos = defaultdeltaPos;
        defalutOption = LanguageManager.Instance.nowOption;
    }

    // Update is called once per frame
    void Update()
    {
        nowOption = LanguageManager.Instance.nowOption;
        if(nowOption!= defalutOption)
        {
            canset = true;
            defalutOption = nowOption;
        }
        if(LanguageManager.Instance.nowOption == LanguageOption.Chinese)
        {
            deltaPos = chdeltaPos;
            if (canset)
            {
                firstText.GetComponent<RectTransform>().anchoredPosition3D = startPos;
                secondText.GetComponent<RectTransform>().anchoredPosition3D = chstopPos;
                canset = false;
            }
            firstText.transform.Translate(Vector3.left * speed * Time.deltaTime);
            secondText.transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (firstText.GetComponent<RectTransform>().anchoredPosition.x < startPos.x - deltaPos - 100f)
            {
                firstText.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(chstopPos.x+10f,chstopPos.y,chstopPos.z);
            }
            if (secondText.GetComponent<RectTransform>().anchoredPosition.x < startPos.x - deltaPos - 100f)
            {
                secondText.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(chstopPos.x + 10f, chstopPos.y, chstopPos.z);
            }
        }
        else if(LanguageManager.Instance.nowOption == LanguageOption.English)
        {
            if (canset)
            {
                firstText.GetComponent<RectTransform>().anchoredPosition3D = startPos;
                secondText.GetComponent<RectTransform>().anchoredPosition3D = stopPos;
                canset = false;
            }
            deltaPos = defaultdeltaPos;
            firstText.transform.Translate(Vector3.left * speed * Time.deltaTime);
            secondText.transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (firstText.GetComponent<RectTransform>().anchoredPosition.x < startPos.x - deltaPos)
            {
                firstText.GetComponent<RectTransform>().anchoredPosition3D = stopPos;
            }
            if (secondText.GetComponent<RectTransform>().anchoredPosition.x < startPos.x - deltaPos)
            {
                secondText.GetComponent<RectTransform>().anchoredPosition3D = stopPos;
            }
        }
        
    }
}
