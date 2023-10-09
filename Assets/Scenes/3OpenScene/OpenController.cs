using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenController : MonoBehaviour
{
    public LevelNewIntro levelNewIntro;
    public GameObject[] upCanvasSmall;
    public GameObject[] upCanvasWhole;
    public Image[] images;
    public Text[] texts;
    private void Start()
    {
        SaveManager.Instance.LoadLevel();
        if (!SaveManager.Instance.IsOpenEnd)
        {
            levelNewIntro.SetActived();
            SetAllFalse(upCanvasWhole);
        }else if (SaveManager.Instance.IsOpenEnd && !SaveManager.Instance.IsLevelOneEnd)
        {
            SetAllFalse(upCanvasWhole);
            SetAllTrue(upCanvasSmall);
            levelNewIntro.SetDeactived();
        }else if(SaveManager.Instance.IsOpenEnd && SaveManager.Instance.IsLevelOneEnd)
        {
            levelNewIntro.SetDeactived();
            SetAllFalse(upCanvasSmall);
            SetAllTrue(upCanvasWhole);
        }
    }
    private void SetAllFalse(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }
    private void SetAllTrue(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }
    private void Update()
    {
        if (SaveManager.Instance.IsOpenEnd && !SaveManager.Instance.IsLevelOneEnd)
        {
            levelNewIntro.SetDeactived();
            SetAllTrue(upCanvasSmall);
        }else if(SaveManager.Instance.IsOpenEnd && SaveManager.Instance.IsLevelOneEnd)
        {
            levelNewIntro.SetDeactived();
            SetAllFalse(upCanvasSmall);
            SetAllTrue(upCanvasWhole);
        }

    }
    public void ImageChange()
    {
        foreach (var image in images)
        {
            image.color = new Color(0f, 0f, 0f, 0f);
        }
        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
        }
    }
    public void ReturnIntro()
    {
        TransManager.Instance.ChangeScene("IntroScene");
        SaveManager.Instance.SaveLevelFalse();
        ImageChange();
    }
    public void ReturnHome()
    {
        TransManager.Instance.ChangeScene("TitleScene");
        ImageChange();
    }
    public void PlayClick()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.CardPlace);
    }
}
