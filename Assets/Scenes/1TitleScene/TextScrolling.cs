using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScrolling : MonoBehaviour
{
    public GameObject firstText;
    public GameObject secondText;
    public float speed = 10f;
    private float defaultdeltaPos;
    private Vector3 startPos;
    private Vector3 stopPos;
    private void Start()
    {
        startPos = firstText.GetComponent<RectTransform>().anchoredPosition3D;
        stopPos = secondText.GetComponent<RectTransform>().anchoredPosition3D;
        defaultdeltaPos = stopPos.x - startPos.x;
    }
    private void Update()
    {
        ProcessTextForLanguage(firstText, defaultdeltaPos, stopPos);
        ProcessTextForLanguage(secondText, defaultdeltaPos, stopPos);
    }
    private void ProcessTextForLanguage(GameObject textObject, float deltaPos, Vector3 stopPos)
    {
        var rectTransform = textObject.GetComponent<RectTransform>();
        textObject.transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(rectTransform.anchoredPosition.x < startPos.x - deltaPos)
        {
            rectTransform.anchoredPosition3D = stopPos;
        }
    }
}
