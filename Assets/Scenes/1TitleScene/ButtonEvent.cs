using UnityEngine;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{
    /// <summary>
    /// 鼠标离开后将颜色恢复
    /// </summary>
    public void TextSet()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
    }
    /// <summary>
    /// `开始菜单`中按钮Pressed后回到原来的颜色
    /// </summary>
    public void OnMouseClick()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    /// <summary>
    /// `回到标题`Pressed后回到原来的颜色
    /// </summary>
    public void OnMouseClickTwo()
    {
        GetComponentInChildren<Text>().color = new Color(0.7773585f, 0.8399767f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    /// <summary>
    /// 当鼠标碰到`开始菜单`中的按钮，Text变色+播放音频
    /// </summary>
    public void OnMouseSelected()
    {
        GetComponentInChildren<Text>().color = Color.black;
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    /// <summary>
    /// 当鼠标碰到`回到标题`按钮，Text变色+播放音频
    /// </summary>
    public void OnMouseSelectedTwo()
    {
        GetComponentInChildren<Text>().color = new Color(0.3f, 0.5f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    /// <summary>
    /// 当鼠标离开`回到标题`按钮，Text变回原来的颜色
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
