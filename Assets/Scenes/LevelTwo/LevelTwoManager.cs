using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTwoManager : MonoBehaviour
{
    //public Image BGpic;
    //public int wave = 1;
    //public bool iswave;
    //[Header("Settings")]
    //[SerializeField] private GameObject go;

    //[Header("RandomDelay")]
    //[SerializeField] private float minRandomDelay;
    //[SerializeField] private float maxRandomDelay;

    //private float _spawnTimer = 0f;
    //private int _enemiesSpawned;

    //private float WaitingTime = 0f;
    //private void Start()
    //{
    //    WaitingTime = 0f;
    //    wave = 1;
    //}

    //private void Update()
    //{
    //    if (iswave)
    //    {
    //        if (wave == 1)
    //        {
    //            int rand = Random.Range(wave, wave + 2);
    //            rand = rand * 4;
    //            for (int i = 0; i < rand; i++)
    //            {
    //                _spawnTimer -= Time.deltaTime;
    //                if (_spawnTimer <= 0)
    //                {
    //                    _spawnTimer = GetSpawnDelay();
    //                    _enemiesSpawned++;
    //                    SpawnEnemy();
    //                }
    //            }
    //            WaitingTime += Time.deltaTime;
    //            if (WaitingTime > 40f && wave == 1)
    //            {
    //                wave = 2;
    //            }
    //        }
    //        if (wave == 2)
    //        {
    //            int rand = Random.Range(wave, wave + 2 * 2);
    //            rand = rand * 4;
    //            for (int i = 0; i < rand; i++)
    //            {
    //                _spawnTimer -= Time.deltaTime;
    //                if (_spawnTimer <= 0)
    //                {
    //                    _spawnTimer = GetSpawnDelay();
    //                    _enemiesSpawned++;
    //                    SpawnEnemy();
    //                }
    //            }
    //            WaitingTime += Time.deltaTime;
    //            if (WaitingTime > 80f && wave == 2)
    //            {
    //                wave = 3;
    //            }
    //        }
    //        if (wave == 3)
    //        {
    //            int rand = Random.Range(wave, wave + 2 * 2);
    //            rand = rand * 4;
    //            for (int i = 0; i < rand; i++)
    //            {
    //                _spawnTimer -= Time.deltaTime;
    //                if (_spawnTimer <= 0)
    //                {
    //                    _spawnTimer = GetSpawnDelay();
    //                    _enemiesSpawned++;
    //                    SpawnEnemy();
    //                }
    //            }
    //            WaitingTime += Time.deltaTime;
    //            if (WaitingTime > 60f && wave == 3)
    //            {
    //                wave = 4;
    //            }
    //        }
    //        if (wave == 4)
    //        {
    //            bool isnext = true;
    //            //先遍历所有的物体
    //            Scene scene = SceneManager.GetActiveScene();
    //            List<GameObject> allObj = new List<GameObject>(scene.GetRootGameObjects());
    //            List<GameObject> newObj = new List<GameObject>();
    //            foreach (GameObject singleObj in allObj)
    //            {
    //                newObj.Add(singleObj);
    //                FindAll(singleObj.transform, ref newObj);
    //            }
    //            foreach (GameObject singleObj in newObj)
    //            {
    //                if (singleObj.GetComponent<EnemyLO>() != null)
    //                {
    //                    if (singleObj.activeInHierarchy) isnext = false;
    //                }
    //            }
    //            if (isnext)
    //            {
    //                TransManager.Instance.ChangeScene("Successful");
    //            }


    //        }
    //    }
    //}

    //private void FindAll(Transform parent, ref List<GameObject> newObj)
    //{
    //    for (int i = 0; i < parent.childCount; i++)
    //    {
    //        Transform temp = parent.GetChild(i);
    //        newObj.Add(temp.gameObject);
    //        if (temp.childCount > 0)
    //        {
    //            FindAll(temp, ref newObj);
    //        }
    //    }
    //}
    //private void SpawnEnemy()
    //{
    //        GameObject prf = GameObject.Instantiate(go);
    //        prf.SetActive(true);
    //}
    //private float GetSpawnDelay()
    //{
    //    return Random.Range(minRandomDelay, maxRandomDelay);
    //}
    //public void WaveStart()
    //{
    //    iswave = true;
    //}
    //public void LoseFinger()
    //{
    //    CursorManager.Instance.AddCoins(30);
    //    Material mat = BGpic.GetComponent<Image>().material;
    //    mat.SetFloat("_OverlayGlow", 0f);
    //    AudioManager.Instance.PlaySFX(SoundEffect.Mousehit);
    //    mat.SetFloat("_OverlayGlow", 0.4f);
    //    mat.SetFloat("_ShakeUvSpeed", 1f);
    //    Invoke(nameof(DestroyWhol), 0.2f);
    //}
    //public void DestroyWhol()
    //{
    //    AudioManager.Instance.PlaySFX(SoundEffect.Stronghit);
    //    Material mat = BGpic.GetComponent<Image>().material;
    //    mat.SetFloat("_OverlayGlow", 0.8f);
    //    mat.SetFloat("_OverlayGlow", 0f);
    //    mat.SetFloat("_ShakeUvSpeed", 0f);
    //}
}
