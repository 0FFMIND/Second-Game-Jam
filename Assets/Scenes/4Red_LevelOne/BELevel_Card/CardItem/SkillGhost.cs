using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGhost : MonoBehaviour
{
    public static SkillGhost Instance;
    public bool isCardOne;
    public bool isCardTwo;
    public static bool isCardThree;
    public bool isCardFour;
    public bool isCardSeven;
    public bool isCardEight;
    public bool isCardNine;
    public bool isCardTen;
    private SpriteRenderer spriteRender;
    private int id;
    public BuildingGhost buildingGhost;
    public bool OneOnlyOnce = false;
    public bool TwoOnlyOnce = false;
    public GameObject[] allSkill;
    public ResourceTypeSO power;
    float TimerTen;
    float TimerNine;
    float TimerEight;
    float TimerSeven;
    float TimerFour;
    public float TimerThree;
    public float TimerTwo;
    public float TimerOne;
    bool onlyOneOne = true;
    bool onlyTwoOne = true;
    bool onlyThreeOne = true;
    bool onlyFourOne = true;
    bool onlySevenOne = true;
    bool onlyEightOne = true;
    bool onlyNineOne = true;
    bool onlyTenOne = true;
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
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardOne = true;
            onlyOneOne = true;
        }
        if (id == 2)
        {
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardTwo = true;
            onlyTwoOne = true;
        }
        if (id == 3)
        {
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardThree = true;
            onlyThreeOne = true;
        }
        if(id == 4)
        {
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardFour = true;
            onlyFourOne = true;
        }
        if (id == 10)
        {
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardTen = true;
            onlyTenOne = true;
        }
        if (id == 7)
        {
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardSeven = true;
            onlySevenOne = true;
        }
        if (id == 8)
        {
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardEight = true;
        }
        if(id == 9)
        {
            if (obj.GetComponent<Image>().fillAmount < 1) return;
            obj.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 0.5f);
            obj.GetComponent<Image>().fillAmount = 0;
            isCardNine = true;
            onlyNineOne = true;
        }
    }

    private void Update()
    {
        if (isCardFour)
        {
            if (onlyFourOne)
            {
                if (GameObject.FindGameObjectsWithTag("solar") != null && GameObject.FindGameObjectsWithTag("solar").Length != 0)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("solar").Length; i++)
                    {
                        GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<ResourceGenerator>().produce += 20;
                    }
                }
                if (GameObject.FindGameObjectsWithTag("windstation") != null && GameObject.FindGameObjectsWithTag("windstation").Length != 0)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("windstation").Length; i++)
                    {
                        GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<ResourceGenerator>().produce += 20;
                    }
                }
            }
            if (onlyFourOne)
            {
                TimerFour = 0f;
                onlyFourOne = false;
            }
            else if (!onlyFourOne)
            {
                TimerFour += Time.deltaTime;
            }
            if (TimerFour > 3f)
            {
                isCardFour = false;
                if (GameObject.FindGameObjectsWithTag("solar") != null && GameObject.FindGameObjectsWithTag("solar").Length != 0)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("solar").Length; i++)
                    {
                        GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<ResourceGenerator>().produce -= 20;
                    }
                }
                if (GameObject.FindGameObjectsWithTag("windstation") != null && GameObject.FindGameObjectsWithTag("windstation").Length != 0)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("windstation").Length; i++)
                    {
                        GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<ResourceGenerator>().produce -= 20;
                    }
                }
            }


        }
        if (isCardTwo)
        {
            if (onlyTwoOne)
            {
                TimerTwo = 0f;
                onlyTwoOne = false;
            }
            else if (!onlyTwoOne)
            {
                TimerTwo += Time.deltaTime;
            }
            if (TimerTwo > 3f)
            {
                isCardTwo = false;
            }
            if (GameObject.FindGameObjectsWithTag("solar") != null && GameObject.FindGameObjectsWithTag("solar").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("solar").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<ResourceGenerator>().timer += Time.deltaTime;
                }
            }
            if (GameObject.FindGameObjectsWithTag("windstation") != null && GameObject.FindGameObjectsWithTag("windstation").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("windstation").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("windstation")[i].GetComponent<ResourceGenerator>().timer += Time.deltaTime;
                }
            }
            if (GameObject.FindGameObjectsWithTag("collector") != null && GameObject.FindGameObjectsWithTag("collector").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("collector").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("collector")[i].GetComponent<ResourceGenerator>().timer += Time.deltaTime;
                }
            }
        }
        if (isCardThree)
        {
            if (onlyThreeOne)
            {
                TimerThree = 0f;
                onlyThreeOne = false;
            }
            else if (!onlyThreeOne)
            {
                TimerThree += Time.deltaTime;
            }
            if (TimerThree > 3f)
            {
                isCardThree = false;
            }
        }
        if (isCardNine)
        {
            List<float> speeds = new List<float>();
            float speed = 0f;
            if (GameObject.FindGameObjectWithTag("shootcenter") != null)
            {
                GameObject shootcenter = GameObject.FindGameObjectWithTag("shootcenter");

                if (onlyNineOne)
                {
                    shootcenter.GetComponentInChildren<TankTurretProjectile>().DelayPerShot = shootcenter.GetComponentInChildren<TankTurretProjectile>().DelayPerShot * 0.5f;
                }
            }
            if (GameObject.FindGameObjectsWithTag("turret") != null)
            {
                GameObject[] turrets = GameObject.FindGameObjectsWithTag("turret");

                for (int i = 0; i < turrets.Length; i++)
                {
                    if (onlyNineOne)
                    {
                        turrets[i].GetComponentInChildren<TankTurretProjectile>().DelayPerShot = turrets[i].GetComponentInChildren<TankTurretProjectile>().DelayPerShot * 0.5f;
                    }
                }
            }
            if (onlyNineOne)
            {
                TimerNine = 0f;
                onlyNineOne = false;
            }
            else if (!onlyNineOne)
            {
                TimerNine += Time.deltaTime;
            }

            if (TimerNine > 3f)
            {
                isCardNine = false;
                if (GameObject.FindGameObjectsWithTag("turret") != null)
                {
                    GameObject[] turrets = GameObject.FindGameObjectsWithTag("turret");
                    for (int i = 0; i < turrets.Length; i++)
                    {
                        turrets[i].GetComponentInChildren<TankTurretProjectile>().DelayPerShot = turrets[i].GetComponentInChildren<TankTurretProjectile>().DelayPerShot * 2f;
                    }
                }
                if (GameObject.FindGameObjectWithTag("shootcenter") != null)
                {
                    GameObject shootcenter = GameObject.FindGameObjectWithTag("shootcenter");
                    shootcenter.GetComponentInChildren<TankTurretProjectile>().DelayPerShot = shootcenter.GetComponentInChildren<TankTurretProjectile>().DelayPerShot * 2f;
                }
            }
        }
        if (isCardOne)
        {
            List<float> speeds = new List<float>();
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && onlyOneOne)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    if (onlyOneOne)
                    {
                        speeds.Add(enemies[i].GetComponent<PathFollower>().Speed);
                    }
                }
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<PathFollower>().Speed = speeds[i] * 0.8f;
                }
            }
            if (onlyOneOne)
            {
                TimerOne = 0f;
                onlyOneOne = false;
            }
            else if (!onlyOneOne)
            {
                TimerOne += Time.deltaTime;
            }
            
            if (TimerOne > 3f)
            {
                isCardOne = false;
                if (GameObject.FindGameObjectsWithTag("Enemy") != null && speeds.Count != 0)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<PathFollower>().Speed = speeds[i];
                    }
                }
            }
        }
        
        if (isCardTen)
        { 
            List<float> speeds = new List<float>();
            if (GameObject.FindGameObjectsWithTag("Enemy") != null)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                try
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (onlyTenOne)
                        {
                            speeds.Add(enemies[i].GetComponent<PathFollower>().Speed);
                        }
                    }
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<PathFollower>().Speed = speeds[i] * 0.7f;
                    }
                }
                catch(Exception e)
                {

                }

            }
            if (onlyTenOne)
            {
                TimerTen = 0f;
                onlyTenOne = false;
            }else if (!onlyTenOne)
            {
                TimerTen += Time.deltaTime;
            }
            
            if (TimerTen > 2f)
            {
                isCardTen = false;
                if (GameObject.FindGameObjectsWithTag("Enemy") != null)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    try
                    {
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            enemies[i].GetComponent<PathFollower>().Speed = speeds[i];
                        }
                    }
                    catch(Exception e)
                    {

                    }
                }
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerTen > 0f && TimerTen < 0.01f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<HealthSystem>().Damage(5);
                    enemies[i].GetComponent<Enemy>().DealDamage(5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerTen > 0.5f && TimerTen < 0.51f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<HealthSystem>().Damage(10);
                    enemies[i].GetComponent<Enemy>().DealDamage(10);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerTen > 1f && TimerTen < 1.01f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<HealthSystem>().Damage(5);
                    enemies[i].GetComponent<Enemy>().DealDamage(5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerTen > 1.5f && TimerTen < 1.51f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<HealthSystem>().Damage(10);
                    enemies[i].GetComponent<Enemy>().DealDamage(10);
                }
            }
        }
        if (isCardSeven)
        {
            if (onlySevenOne)
            {
                TimerSeven = 0f;
                onlySevenOne = false;
            }
            else if (!onlySevenOne)
            {
                TimerSeven += Time.deltaTime;
            }
            if (TimerSeven > 2f)
            {
                isCardSeven = false;
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerSeven > 0f && TimerSeven < 0.01f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                        enemies[i].GetComponent<HealthSystem>().Damage(5);
                    enemies[i].GetComponent<Enemy>().DealDamage(5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerSeven > 0.5f && TimerSeven < 0.51f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<HealthSystem>().Damage(10);
                    enemies[i].GetComponent<Enemy>().DealDamage(10);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerSeven > 1f && TimerSeven < 1.01f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<HealthSystem>().Damage(20);
                    enemies[i].GetComponent<Enemy>().DealDamage(20);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Enemy") != null && TimerSeven > 1.5f && TimerSeven < 1.51f)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<HealthSystem>().Damage(5);
                    enemies[i].GetComponent<Enemy>().DealDamage(5);
                }
            }
        }
        if (isCardEight)
        {
            ResourceManager.Instance.AddResource(power, 50);
            isCardEight = false;
        }
            foreach (var skill in allSkill)
        {
            if (skill.GetComponent<Image>().fillAmount == 1)
            {
                skill.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                skill.GetComponent<Image>().fillAmount += Time.deltaTime * 0.05f;
            }
        }
        
        if (GameObject.FindGameObjectWithTag("shootcenter") != null)
        {
            TwoOnlyOnce = true;
        }
        if (GameObject.FindGameObjectWithTag("defenseholder") != null)
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
