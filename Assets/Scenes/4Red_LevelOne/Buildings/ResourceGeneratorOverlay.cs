using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform bar;

    private void Start()
    {
         bar = transform.Find("bar");
    }

    private void Update()
    {
        if(resourceGenerator.gameObject.tag == "collector")
        {
            bar.localScale = new Vector3(0.8833306f * resourceGenerator.GetTimerNormalized(), 0.4811428f, 1f);
        }
        if(resourceGenerator.gameObject.tag == "windstation" || resourceGenerator.gameObject.tag == "turret")
        {
            bar.localScale = new Vector3(0.8833306f * resourceGenerator.GetNormalized(), 0.4811428f, 1f);
        }
        bar.localScale = new Vector3(0.8833306f * resourceGenerator.GetTimerNormalized(), 0.4811428f, 1f);
    }
}
