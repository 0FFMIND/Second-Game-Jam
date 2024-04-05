using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DenfenseTower : MonoBehaviour
{
    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 10f)
        {
            timer = 0;
            if (GameObject.FindGameObjectsWithTag("solar") != null && GameObject.FindGameObjectsWithTag("solar").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("solar").Length; i++)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Health);
                    GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<Building>().healthSystem.Damage(-5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("windstation") != null && GameObject.FindGameObjectsWithTag("windstation").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("windstation").Length; i++)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Health);
                    GameObject.FindGameObjectsWithTag("windstation")[i].GetComponent<Building>().healthSystem.Damage(-5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("collector") != null && GameObject.FindGameObjectsWithTag("collector").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("collector").Length; i++)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Health);
                    GameObject.FindGameObjectsWithTag("collector")[i].GetComponent<Building>().healthSystem.Damage(-5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Regulator") != null && GameObject.FindGameObjectsWithTag("Regulator").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Regulator").Length; i++)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Health);
                    GameObject.FindGameObjectsWithTag("Regulator")[i].GetComponent<Building>().healthSystem.Damage(-5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("turret") != null && GameObject.FindGameObjectsWithTag("turret").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("turret").Length; i++)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Health);
                    GameObject.FindGameObjectsWithTag("turret")[i].GetComponent<Building>().healthSystem.Damage(-5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("defenseholder") != null && GameObject.FindGameObjectsWithTag("defenseholder").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("defenseholder").Length; i++)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Health);
                    GameObject.FindGameObjectsWithTag("defenseholder")[i].GetComponent<Building>().healthSystem.Damage(-5);
                }
            }
            if (GameObject.FindGameObjectsWithTag("shootcenter") != null && GameObject.FindGameObjectsWithTag("shootcenter").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("shootcenter").Length; i++)
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.Health);
                    GameObject.FindGameObjectsWithTag("shootcenter")[i].GetComponent<Building>().healthSystem.Damage(-5);
                }
            }
            if (SceneManager.GetActiveScene().name == "LevelThree" || SceneManager.GetActiveScene().name == "LevelFour" || SceneManager.GetActiveScene().name == "LevelFive" || SceneManager.GetActiveScene().name == "LevelSix")
            {
                if (GameObject.FindGameObjectsWithTag("base") != null)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("base").Length; i++)
                    {
                        AudioManager.Instance.PlaySFX(SoundEffect.Health);
                        GameObject.FindGameObjectsWithTag("base")[i].GetComponent<Building>().healthSystem.Damage(-5);
                    }
                }
            }
        }
    }
}
