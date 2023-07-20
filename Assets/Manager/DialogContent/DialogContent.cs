using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDialogContent", menuName = "NewDialog")]
[System.Serializable]
public class DialogContent : ScriptableObject
{
    public bool istypped;
    public List<string> dialogList;
}
