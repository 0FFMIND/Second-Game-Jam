using UnityEngine;
using UnityEngine.UI;
public class BuildingGhost : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        spriteRender = this.GetComponent<SpriteRenderer>();
        Hide();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if(e.activeBuildingType == null)
        {
            Hide();
        }
        else
        {
            Show(e.activeBuildingType.prefab.gameObject.GetComponent<SpriteRenderer>().sprite);
        }
    }

    private void Update()
    {
        transform.position = GetMouseWorldPosition();
    }
    private void Hide()
    {
        spriteRender.color = new Color(1, 1, 1, 0);
    }
    private void Show(Sprite ghostSprite)
    {
        spriteRender.color = new Color(1, 1, 1, 0.5f);
        spriteRender.sprite = ghostSprite;

    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(cursorPos.x, cursorPos.y, 1f);
    }
}
