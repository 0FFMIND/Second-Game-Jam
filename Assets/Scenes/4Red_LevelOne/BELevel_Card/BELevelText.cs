using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELevelText : MonoBehaviour
{
    public GameObject[] introCG;
    public GameObject teamSelect;
    public GameObject introTextOne;
    public GameObject introTextTwo;
    public GameObject introTextThree;
    public GameObject introCG2;
    public DialogContent IntroDialog;
    public static bool isFinished = false;
    public void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.IntroScene);
        introTextTwo.SetActive(false);
        introTextThree.SetActive(false);
        teamSelect.SetActive(false);
        introCG[0].SetActive(true);
        introCG2.SetActive(false);
        DialogManager.Instance.isIntroFinished = false;
        DialogManager.Instance.isBEfinished = true;
        DialogManager.Instance.Init("Intro", IntroDialog);
    }
    // ��TextAnimator�õ��������
    public void PlayTypping()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Typing);
    }
    public void SetFinishedFalse()
    {
        isFinished = false;
    }
    public void SetFinished()
    {
        isFinished = true;
    }
    public void Update()
    {
        // ѡ���������ڿ��ͳ�������
        if(DialogManager.Instance.isIntroFinished && SaveManager.Instance.IsAngel != 0 && teamSelect.activeSelf)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Beep);
            teamSelect.SetActive(false);
            introCG2.SetActive(true);
        }
        if (!DialogManager.Instance.isIntroFinished && DialogManager.Instance.index == 3)//��dialogManager�����ź�
        {
            introCG[0].SetActive(false);introCG[1].SetActive(true);
        }
        if (DialogManager.Instance.isIntroFinished)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Beep);
            SaveManager.Instance.LoadLevel();
            if(SaveManager.Instance.IsAngel != 0 && introCG[1].activeSelf)
            {
                introTextOne.SetActive(false);introTextTwo.SetActive(true);
                introTextThree.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    TransManager.Instance.ChangeScene("LevelOne");
                }
            }
            if(SaveManager.Instance.IsAngel == 0)
            {
                introCG[1].SetActive(false); introTextTwo.SetActive(true); introTextOne.SetActive(false); teamSelect.SetActive(true);
            }
            // �Ѿ�ѡ������ֽ������ֹؿ�

        }
    }
}
