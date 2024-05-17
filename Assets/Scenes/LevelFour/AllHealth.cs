using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class AllHealth : MonoBehaviour
{
    float timer = 0;

    UnityAction pauseHealthMinus;

    public int RegulatorCount;
    public bool isWeatherClear = false;

    private void Awake()
    {
        pauseHealthMinus = new UnityAction(() =>
        {
            if (GameObject.FindGameObjectsWithTag("Regulator").Length == RegulatorCount)
            {
                isWeatherClear = true;
            }
        });
    }
    private void Start()
    {
        EventManager.Instance.AddEventListener("PauseHealthMinus", pauseHealthMinus);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 4f)
        {
            LoseHealth();
            timer = 0;
        }
    }
    private void LoseHealth()
    {
<<<<<<< HEAD
        if (true)
=======
        EventManager.Instance.EventTrigger("PauseHealthMinus");

        if (!isWeatherClear)
>>>>>>> 2dc9f73cd6d9112f7449c910a527469d8ac7e3f5
        {

            if (GameObject.FindGameObjectsWithTag("solar") != null && GameObject.FindGameObjectsWithTag("solar").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("solar").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<Building>().healthSystem.Damage(1);
                }
            }
            if (GameObject.FindGameObjectsWithTag("windstation") != null && GameObject.FindGameObjectsWithTag("windstation").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("windstation").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("windstation")[i].GetComponent<Building>().healthSystem.Damage(1);
                }
            }
            if (GameObject.FindGameObjectsWithTag("collector") != null && GameObject.FindGameObjectsWithTag("collector").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("collector").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("collector")[i].GetComponent<Building>().healthSystem.Damage(1);
                }
            }
            if (GameObject.FindGameObjectsWithTag("Regulator") != null && GameObject.FindGameObjectsWithTag("Regulator").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Regulator").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("Regulator")[i].GetComponent<Building>().healthSystem.Damage(1);
                }
            }
            if (GameObject.FindGameObjectsWithTag("turret") != null && GameObject.FindGameObjectsWithTag("turret").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("turret").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("turret")[i].GetComponent<Building>().healthSystem.Damage(1);
                }
            }
            if (GameObject.FindGameObjectsWithTag("defenseholder") != null && GameObject.FindGameObjectsWithTag("defenseholder").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("defenseholder").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("defenseholder")[i].GetComponent<Building>().healthSystem.Damage(1);
                }
            }
            if (GameObject.FindGameObjectsWithTag("shootcenter") != null && GameObject.FindGameObjectsWithTag("shootcenter").Length != 0)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("shootcenter").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("shootcenter")[i].GetComponent<Building>().healthSystem.Damage(1);
                }
            }
            if (SceneManager.GetActiveScene().name == "LevelThree" || SceneManager.GetActiveScene().name == "LevelFour" || SceneManager.GetActiveScene().name == "LevelFive")
            {
                if (GameObject.FindGameObjectsWithTag("base") != null)
                {
                    for (int i = 0; i < GameObject.FindGameObjectsWithTag("base").Length; i++)
                    {
                        GameObject.FindGameObjectsWithTag("base")[i].GetComponent<Building>().healthSystem.Damage(1);
                    }
                }
            }
        }
    }
}
