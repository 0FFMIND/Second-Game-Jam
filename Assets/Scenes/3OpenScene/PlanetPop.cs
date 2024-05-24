using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetPop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject information;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SaveManager.Instance.IsOpenEnd)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.PopUp);
            information.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (SaveManager.Instance.IsOpenEnd)
        {
            if (information != null)
            {
                information.SetActive(false);
            }
        }
    }
}
