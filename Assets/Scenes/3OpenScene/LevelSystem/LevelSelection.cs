using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // ���ص�ÿһ��С������
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
        else if (isUnlocked) // �������˻ҵ�ͼ���ͱ��ԭɫ
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
            else if(!PlayerPrefs.HasKey(levelName)) // ���PlayerPrefsû����ֵ���Ǿ��ǻ�û��ȥ��ؿ�
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
