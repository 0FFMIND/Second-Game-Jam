using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/New Dialog")]
[System.Serializable]
public class DialogContent : ScriptableObject
{
    public List<string> CNdialogList;
    public List<string> ENdialogList;
}
