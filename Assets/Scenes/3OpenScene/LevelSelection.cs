using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // ���ص�ÿһ��С������
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
    // �����жϹؿ��Ƿ���Խ�������isUnlocked����
    private void UnlockLevel()
    {
        if (preLevel == null)
        {
            isUnlocked = true;
            return;
        }
        foreach (var level in preLevel)
        {
            if (!level.isFinished) // TO DO���ڹؿ���������
            {
                isUnlocked = false;
                return;
            }
        }
        isUnlocked = true;
    }
    // ��isUnlocked�����ùؿ��Ŀ��Ӷ�/������ɫ
    private void UpdateLevelImage()
    {
        if (!isUnlocked) // �����ɽ�����ʱ�������ǲ��ɼ��ģ����ǿ��Լ�����ҵ�ͼ��ͼ��
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
            if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
                else if (!SaveManager.Instance.IsLevelTwoEnd) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
            if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
                else if (!SaveManager.Instance.isLevelFourEnd) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
            if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
                else if (!SaveManager.Instance.isLevelFiveEnd) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
            if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
                else if (!SaveManager.Instance.isLevelSixEnd) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
            if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
                else if (!SaveManager.Instance.isLevelSixEnd) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
            if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
                else if (!SaveManager.Instance.isLevelThreeEnd) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
            if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
                else if (!SaveManager.Instance.IsLevelOneEnd) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
