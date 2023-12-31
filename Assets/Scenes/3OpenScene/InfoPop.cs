using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InfoPop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject information;
    public string gameScene;
    public LevelSelection levelSelection;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SaveManager.Instance.IsOpenEnd)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.PopUp);
            if(gameScene != "" && levelSelection.isUnlocked)
            {
                EventManager.Instance.EventTrigger("ChangeImg", this);
                information.SetActive(false);
                TransManager.Instance.ChangeScene(gameScene);
            }
        }
    }
    private bool CanInfoPop()
    {
        if (!levelSelection.isUnlocked) return false;
        else return true;
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
            if(SceneManager.GetActiveScene().name == "OpenScene" && information != null)
            {
                information.SetActive(false);
            }
        }
    }
}
