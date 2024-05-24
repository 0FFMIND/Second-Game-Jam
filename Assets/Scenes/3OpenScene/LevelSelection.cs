using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // 挂载到每一个小关上面
    public bool isUnlocked;
    public bool isFinished;
    public string levelName;
    public GameObject[] stars;
    public LevelSelection[] preLevel;
    public GameObject[] icons;
    public GameObject pollution;
    public GameObject path;
    public GameObject infoPoP;
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
        if (levelName == "LevelTwo")
        {
            if (!isUnlocked)
            {
                path.SetActive(false);
                infoPoP.SetActive(false);
            }
            if (SaveManager.Instance.IsLevelTwoEnd)
            {
                isFinished = true;
            }
            if (isUnlocked) // 当解锁了灰的图案就变回原色
            {
                foreach (var icon in icons)
                {
                    icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (SaveManager.Instance.IsLevelTwoEnd)
                {
                    for (int i = 0; i < SaveManager.Instance.LevelTwoStar; i++)
                    {
                        path.SetActive(true);
                        pollution.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                    }
                }
                else if (!SaveManager.Instance.IsLevelTwoEnd) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
                {
                    for (int i = 0; i < stars.Length; i++)
                    {
                        path.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
                    }
                }
            }
        }
        if (levelName == "LevelFour")
        {
            if (!isUnlocked)
            {
                path.SetActive(false);
                infoPoP.SetActive(false);
            }
            if (SaveManager.Instance.isLevelFourEnd)
            {
                isFinished = true;
            }
            if (isUnlocked) // 当解锁了灰的图案就变回原色
            {
                foreach (var icon in icons)
                {
                    icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (SaveManager.Instance.isLevelFourEnd)
                {
                    for (int i = 0; i < SaveManager.Instance.LevelOneStar; i++)
                    {
                        path.SetActive(true);
                        pollution.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                    }
                }
                else if (!SaveManager.Instance.isLevelFourEnd) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
                {
                    for (int i = 0; i < stars.Length; i++)
                    {
                        path.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
                    }
                }
            }
        }
        if (levelName == "LevelFive")
        {
            if (!isUnlocked)
            {
                path.SetActive(false);
                infoPoP.SetActive(false);
            }
            if (SaveManager.Instance.isLevelFiveEnd)
            {
                isFinished = true;
            }
            if (isUnlocked) // 当解锁了灰的图案就变回原色
            {
                foreach (var icon in icons)
                {
                    icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (SaveManager.Instance.isLevelFiveEnd)
                {
                    for (int i = 0; i < SaveManager.Instance.LevelOneStar; i++)
                    {
                        path.SetActive(true);
                        pollution.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                    }
                }
                else if (!SaveManager.Instance.isLevelFiveEnd) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
                {
                    for (int i = 0; i < stars.Length; i++)
                    {
                        path.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
                    }
                }
            }
        }
        if (levelName == "LevelSix")
        {
            if (!isUnlocked)
            {
                path.SetActive(false);
                infoPoP.SetActive(false);
            }
            if (SaveManager.Instance.isLevelSixEnd)
            {
                isFinished = true;
            }
            if (isUnlocked) // 当解锁了灰的图案就变回原色
            {
                foreach (var icon in icons)
                {
                    icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (SaveManager.Instance.isLevelSixEnd)
                {
                    for (int i = 0; i < SaveManager.Instance.LevelOneStar; i++)
                    {
                        path.SetActive(true);
                        pollution.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                    }
                }
                else if (!SaveManager.Instance.isLevelSixEnd) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
                {
                    for (int i = 0; i < stars.Length; i++)
                    {
                        path.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
                    }
                }
            }
        }
        if (levelName == "LevelSeven")
        {
            if (!isUnlocked)
            {
                path.SetActive(false);
                infoPoP.SetActive(false);
            }
            if (SaveManager.Instance.isLevelSixEnd)
            {
                isFinished = true;
            }
            if (isUnlocked) // 当解锁了灰的图案就变回原色
            {
                foreach (var icon in icons)
                {
                    icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (SaveManager.Instance.isLevelSixEnd)
                {
                    for (int i = 0; i < SaveManager.Instance.LevelOneStar; i++)
                    {
                        path.SetActive(true);
                        pollution.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                    }
                }
                else if (!SaveManager.Instance.isLevelSixEnd) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
                {
                    for (int i = 0; i < stars.Length; i++)
                    {
                        path.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
                    }
                }
            }
        }
        if (levelName == "LevelThree")
        {
            if (!isUnlocked)
            {
                path.SetActive(false);
                infoPoP.SetActive(false);
            }
            if (SaveManager.Instance.isLevelThreeEnd)
            {
                isFinished = true;
            }
            if (isUnlocked) // 当解锁了灰的图案就变回原色
            {
                foreach (var icon in icons)
                {
                    icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (SaveManager.Instance.isLevelThreeEnd)
                {
                    for (int i = 0; i < SaveManager.Instance.LevelTwoStar; i++)
                    {
                        path.SetActive(true);
                        pollution.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                    }
                }
                else if (!SaveManager.Instance.isLevelThreeEnd) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
                {
                    for (int i = 0; i < stars.Length; i++)
                    {
                        path.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
                    }
                }
            }
        }
        if (levelName == "LevelOne")
        {
            if (SaveManager.Instance.IsLevelOneEnd)
            {
                isFinished = true;
            }
            if (isUnlocked) // 当解锁了灰的图案就变回原色
            {
                foreach (var icon in icons)
                {
                    icon.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (SaveManager.Instance.IsLevelOneEnd)
                {
                    for (int i = 0; i < SaveManager.Instance.LevelOneStar; i++)
                    {
                        pollution.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(119f / 255f, 216f / 255f, 1f, 1f);
                        path.SetActive(true);
                    }
                }
                else if (!SaveManager.Instance.IsLevelOneEnd) // 如果PlayerPrefs没有数值，那就是还没有去玩关卡
                {
                    for (int i = 0; i < stars.Length; i++)
                    {
                        path.SetActive(false);
                        stars[i].gameObject.SetActive(true);
                        stars[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(123f / 255f, 123f / 255f, 123f / 255f, 1f);
                    }
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
