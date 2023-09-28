using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // 挂载到每一个小关上面
    [SerializeField] private bool isUnlocked;
    [SerializeField] private bool isFinished;
    public string levelName;
    public GameObject[] stars;
    public LevelSelection[] preLevel;
    public GameObject[] icons;
    public GameObject pollution;
    private void Update()
    {
        if (SaveManager.Instance.IsOpenEnd)
        {
            for (int i = 0; i < gameObject.GetComponent<Transform>().childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            UnlockLevel();
            UpdateLevelImage();
        }
        else
        {
            for (int i = 0; i < gameObject.GetComponent<Transform>().childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    // 用来判断关卡是否可以解锁，有isUnlocked变量
    private void UnlockLevel()
    {
        if (preLevel == null)
        {
            isUnlocked = true;
            return;
        }
        foreach (var level in preLevel)
        {
            if (!level.isFinished) // TO DO，在关卡里面设置
            {
                isUnlocked = false;
                return;
            }
        }
        isUnlocked = true;
    }
    // 用isUnlocked来设置关卡的可视度/星星颜色
    private void UpdateLevelImage() 
    {
        if (!isUnlocked) // 当不可解锁的时候星星是不可见的，但是可以见到变灰的图标图案
        {
            foreach (var star in stars)
            {
                star.gameObject.SetActive(false);
            }
            foreach (var icon in icons)
            {
                icon.gameObject.SetActive(true);
                icon.gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
            }
        }
        else if (isUnlocked) // 当解锁了灰的图案就变回原色
        {
            foreach (var icon in icons)
            {
                icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (PlayerPrefs.HasKey(levelName))
            {
                for (int i = 0; i < PlayerPrefs.GetInt(levelName); i++)
                {
                    pollution.SetActive(false);
                    stars[i].gameObject.SetActive(true);
                    stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                }
            }
            else if(!PlayerPrefs.HasKey(levelName)) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].gameObject.SetActive(true);
                    stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f /255f, 1f);
                }
            }
        }
    }
    public void PressSelection(string _LevelName)
    {
        if (isUnlocked)
        {
            TransManager.Instance.ChangeScene(_LevelName);
        }
    }
}
