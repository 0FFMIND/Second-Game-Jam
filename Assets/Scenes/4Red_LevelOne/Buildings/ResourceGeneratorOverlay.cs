using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private void Update()
    {
        if(resourceGenerator.gameObject.tag == "collector")
        {
            transform.Find("bar").localScale = new Vector3(0.8833306f * resourceGenerator.GetTimerNormalized(), 0.4811428f, 1f);
        }
        if(resourceGenerator.gameObject.tag == "windstation" || resourceGenerator.gameObject.tag == "turret")
        {
            transform.Find("bar").localScale = new Vector3(0.8833306f * resourceGenerator.GetNormalized(), 0.4811428f, 1f);
        }
        transform.Find("bar").localScale = new Vector3(0.8833306f * resourceGenerator.GetTimerNormalized(), 0.4811428f, 1f);
    }
}
