
public class TransManager : Singleton<TransManager>
{
    public void ChangeScene(string sceneName)
    {
        Transitioner.Instance.TransitionToScene(sceneName);
    }
}
