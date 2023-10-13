using UnityEngine.SceneManagement;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    public float timer = 0;
    private float timerMax;
    public bool isOnce = false;
    public int produce = 20;
    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingType.resourceGeneratorData.timerMax;
        produce = 20;
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.isBuffOne) produce = (int)(1.2f*produce);
        if (SaveManager.Instance.isBuffThree) timerMax *= 0.8f;
        if (LevelOneController.isCard5) timerMax *= 1.3f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (buildingType.prefab.gameObject.tag == "windstation")
        {
            if(timer > timerMax)
            {
                timer = 0;
                isOnce = !isOnce;
                if (!isOnce)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Coins);
                    ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, produce);
                }
            }
        }
        if (buildingType.prefab.gameObject.tag == "turret" || buildingType.prefab.gameObject.tag == "shootcenter" || buildingType.prefab.gameObject.tag == "defenseholder")
        {
            if (timer > timerMax)
            {
                timer = 0;
                isOnce = !isOnce;
                if (!isOnce)
                {
                }
            }
        }
        if (timer > timerMax)
        {
            timer = 0;
            if(buildingType.resourceGeneratorData.resourceType.name == "Power")
            {
                if (buildingType.nameString == "SolarStation")
                {
                    isOnce = !isOnce;
                    if (!isOnce)
                    {
                        AudioManager.Instance.PlaySFX(SoundEffect.Coins);
                        ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, produce);
                    }
                }
            }

            if(buildingType.resourceGeneratorData.resourceType.name == "Water")
            {
                isOnce = !isOnce;
                if (!isOnce)
                {
                    if (SceneManager.GetActiveScene().name == "LevelOne")
                    {
                        AudioManager.Instance.PlaySFX(SoundEffect.Coins);
                        ResourceManager.Instance.isWaterClear = true;
                    }
                    else if (SceneManager.GetActiveScene().name == "LevelTwo")
                    {
                        AudioManager.Instance.PlaySFX(SoundEffect.Coins);
                    }
                }
            }
        }
    }
    public float GetNormalized()
    {
        return timer / timerMax;
    }
    public float GetTimerNormalized()
    {
        if (!isOnce)
        {
            return 0.5f * timer / timerMax;
        }
        else
        {
            return 0.5f + 0.5f * timer / timerMax;
        }
    }
}
