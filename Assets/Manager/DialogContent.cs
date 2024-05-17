using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDialogContent", menuName = "NewDialog")]
[System.Serializable]
public class DialogContent : ScriptableObject
{
    public List<string> CNdialogList;
    public List<string> ENdialogList;
}
