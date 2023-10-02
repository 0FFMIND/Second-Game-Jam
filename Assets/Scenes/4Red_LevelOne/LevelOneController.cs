using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.bgm[1].Stop();
        AudioManager.Instance.PlayBGM(BackgroundMusic.LevelOneScene);
        CardStorePref.Instance.GetCard();
        int id;int level;CardState state;
        foreach (var obj in CardStorePref.Instance.loadPlayerCards)
        {
            id = obj.Key; level = obj.Value; state = CardStorePref.Instance.stateCard[id];
            if(state == CardState.Deck)
            {
                //在场景中生成三个
            }
        }
    }
}
