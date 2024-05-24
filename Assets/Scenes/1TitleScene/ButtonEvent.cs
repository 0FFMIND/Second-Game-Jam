using UnityEngine;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{
    /// <summary>
    /// ����뿪����ɫ�ָ�
    /// </summary>
    public void TextSet()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
    }
    /// <summary>
    /// `��ʼ�˵�`�а�ťPressed��ص�ԭ������ɫ
    /// </summary>
    public void OnMouseClick()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    /// <summary>
    /// `�ص�����`Pressed��ص�ԭ������ɫ
    /// </summary>
    public void OnMouseClickTwo()
    {
        GetComponentInChildren<Text>().color = new Color(0.7773585f, 0.8399767f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    /// <summary>
    /// ���������`��ʼ�˵�`�еİ�ť��Text��ɫ+������Ƶ
    /// </summary>
    public void OnMouseSelected()
    {
        GetComponentInChildren<Text>().color = Color.black;
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    /// <summary>
    /// ���������`�ص�����`��ť��Text��ɫ+������Ƶ
    /// </summary>
    public void OnMouseSelectedTwo()
    {
        GetComponentInChildren<Text>().color = new Color(0.3f, 0.5f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    /// <summary>
    /// ������뿪`�ص�����`��ť��Text���ԭ������ɫ
    /// </summary>
    public void OnMouseExitTwo()
    {
        GetComponentInChildren<Text>().color = new Color(0.7773585f, 0.8399767f, 1f, 1f);
    }
    public void OnDragSlideHandle()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
    }

    public void OnExitSlideHandle()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void OnMouseClickSlide()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
}
