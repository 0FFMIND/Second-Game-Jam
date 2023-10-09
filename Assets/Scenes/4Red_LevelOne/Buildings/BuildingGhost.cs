using UnityEngine;
using UnityEngine.UI;
public class BuildingGhost : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    public bool isSenderOne;
    public bool isSenderTwo;
    public BuildingTypeSO Denfense;
    public BuildingTypeSO Shoot;
    private void Start()
    {
        if(BuildingManager.Instance != null)
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        }
        spriteRender = this.GetComponent<SpriteRenderer>();
        Hide();
    }

    public void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if(e.activeBuildingType == null && !isSenderOne && !isSenderTwo)
        {
            Hide();
            BuildingManager.Instance.activeBuildingType = null;
        }
        else if(e.activeBuildingType != null)
        {
            Show(e.activeBuildingType.prefab.gameObject.GetComponent<SpriteRenderer>().sprite);
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Hide();
            BuildingManager.Instance.activeBuildingType = null;
        }
        if (isSenderOne)
        {
            isSenderOne = false;
            Show(Denfense.prefab.gameObject.GetComponent<SpriteRenderer>().sprite);
            BuildingManager.Instance.activeBuildingType = Denfense;
        }
        if (isSenderTwo)
        {
            isSenderTwo = false;
            Show(Shoot.prefab.gameObject.GetComponent<SpriteRenderer>().sprite);
            BuildingManager.Instance.activeBuildingType = Shoot;
        }
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        }
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
