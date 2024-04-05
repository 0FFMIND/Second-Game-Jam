using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InfoPoPtwo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject information;
    public string gameScene;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SaveManager.Instance.IsOpenEnd)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.PopUp);
            if (gameScene != "")
            {
                EventManager.Instance.EventTrigger("ChangeImg", this);
                information.SetActive(false);
                TransManager.Instance.ChangeScene(gameScene);
            }
        }
    }
    /// <summary>
    /// 可以查看信息的情况下返回true
    /// </summary>
    /// <returns></returns>
    private bool CanInfoPop()
    {
        return true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SaveManager.Instance.IsOpenEnd && CanInfoPop())
        {
            AudioManager.Instance.PlaySFX(SoundEffect.PopUp);
            information.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (SaveManager.Instance.IsOpenEnd && CanInfoPop())
        {
            if (SceneManager.GetActiveScene().name == "OpenScene" && information != null)
            {
                information.SetActive(false);
            }
        }
    }
}
