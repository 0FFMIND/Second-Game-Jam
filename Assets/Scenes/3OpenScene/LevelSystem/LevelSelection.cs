using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // 挂载到每一个小关上面
    [SerializeField] private bool isUnlocked;
    public string levelName;
    public GameObject[] stars;
    public LevelSelection[] preLevel;
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
    private void UnlockLevel()
    {
        if (preLevel == null)
        {
            isUnlocked = true;
            return;
        }
        foreach (var level in preLevel)
        {
            if (!level.isUnlocked)
            {
                isUnlocked = false;
                return;
            }
        }
        isUnlocked = true;
    }
    private void UpdateLevelImage() //UI更新
    {
        if (!isUnlocked)
        {
            foreach (var star in stars)
            {
                star.gameObject.SetActive(!isUnlocked);
            }
        }
        else if (isUnlocked)
        {
            if (PlayerPrefs.HasKey(levelName))
            {
                for (int i = 0; i < PlayerPrefs.GetInt(levelName); i++)
                {
                    // TO DO 改变颜色
                    stars[i].gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    // TO DO 改变颜色
                    stars[i].gameObject.SetActive(false);
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
