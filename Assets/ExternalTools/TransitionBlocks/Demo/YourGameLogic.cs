using UnityEngine;

public class YourGameLogic : MonoBehaviour
{
    public string levelToLoad = "Transition Block Demo";
    public KeyCode _transitionOutKey = KeyCode.T;
    public KeyCode _transitionInKey = KeyCode.Y;
    public KeyCode _transitionOutNoSceneSwitchKey = KeyCode.G;
    public KeyCode _transitionInNoSceneSwitchKey = KeyCode.H;

    void Update()
    {
        if (Input.GetKeyDown(_transitionOutKey))
        {
            Transitioner.Instance.TransitionToScene(levelToLoad);
        }
        else if (Input.GetKeyDown(_transitionInKey))
        {
            Transitioner.Instance.FinishTransition();
        }
        else if(Input.GetKeyDown(_transitionOutNoSceneSwitchKey))
        {
            Transitioner.Instance.TransitionOutWithoutChangingScene();
        }
        else if (Input.GetKeyDown(_transitionInNoSceneSwitchKey))
        {
            Transitioner.Instance.TransitionInWithoutChangingScene();
        }
    }
}
