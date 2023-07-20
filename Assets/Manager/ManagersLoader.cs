using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersLoader : MonoBehaviour
{
    public GameObject AudioPrf;
    public GameObject CursorPrf;
    public GameObject TransPrf;
    public GameObject LangPrf;
    public GameObject SavePrf;
    public GameObject DialogPrf;
    public GameObject GaMPrf;
    public static bool isInit = false;
    private void Awake()
    {
        if (!isInit)
        {
            if ( AudioPrf == null || CursorPrf == null || TransPrf == null || LangPrf == null || SavePrf == null || DialogPrf == null || GaMPrf == null)
            {
                Debug.LogError("Please check your ManagersLoader gameObject");
            }
            else
            {
                if (FindObjectOfType<AudioManager>() != null)
                {
                    Destroy(FindObjectOfType<AudioManager>().gameObject);
                }
                DontDestroyOnLoad(Instantiate(AudioPrf));

                if (FindObjectOfType<CursorManager>() != null)
                {
                    Destroy(FindObjectOfType<CursorManager>().gameObject);
                }
                DontDestroyOnLoad(Instantiate(CursorPrf));

                if (FindObjectOfType<TransManager>() != null)
                {
                    Destroy(FindObjectOfType<TransManager>().gameObject);
                }
                DontDestroyOnLoad(Instantiate(TransPrf));

                if (FindObjectOfType<LanguageManager>() != null)
                {
                    Destroy(FindObjectOfType<LanguageManager>().gameObject);
                }
                DontDestroyOnLoad(Instantiate(LangPrf));

                if (FindObjectOfType<SaveManager>() != null)
                {
                    Destroy(FindObjectOfType<SaveManager>().gameObject);
                }
                DontDestroyOnLoad(Instantiate(SavePrf));

                if (FindObjectOfType<DialogManager>() != null)
                {
                    Destroy(FindObjectOfType<DialogManager>().gameObject);
                }
                DontDestroyOnLoad(Instantiate(DialogPrf));

                if (FindObjectOfType<GameManager>() != null)
                {
                    Destroy(FindObjectOfType<GameManager>().gameObject);
                }
                DontDestroyOnLoad(Instantiate(GaMPrf));
                isInit = true;
            }
        }
    }
}
