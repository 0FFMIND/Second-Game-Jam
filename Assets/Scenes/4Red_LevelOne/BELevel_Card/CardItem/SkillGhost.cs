using UnityEngine;
using UnityEngine.UI;

public class SkillGhost : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    private int id;
    public BuildingGhost buildingGhost;
    public bool OneOnlyOnce = false;
    public bool TwoOnlyOnce = false;
    public GameObject[] allSkill;
    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        Hide();
    }

   
    public void SkillShoot(GameObject obj)
    {
        id = obj.GetComponent<ReadId>().id;
        if (id == 5 || id == 11) return; // 是被动
        else if(id == 6 && !OneOnlyOnce) // 防御中心
        {
            buildingGhost.isSenderOne = true;
        }else if(id == 12 && !TwoOnlyOnce) // 狙击中心
        {
            buildingGhost.isSenderTwo = true;
        }
        if(id == 1)
        {
            if (obj.GetComponent<Image>().fillAmount <= 1) return;
            obj.GetComponent<Image>().fillAmount = 0;
        }
    }

    private void Update()
    {
        foreach (var skill in allSkill)
        {
            if (skill.GetComponent<Image>().fillAmount == 1) return;
            else
            {
                skill.GetComponent<Image>().fillAmount += Time.deltaTime * 0.003f;
            }
        }
        if(GameObject.FindGameObjectWithTag("shootcenter") != null)
        {
            TwoOnlyOnce = true;
        }
        if(GameObject.FindGameObjectWithTag("defenseholder") != null)
        {
            OneOnlyOnce = true;
        }
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
