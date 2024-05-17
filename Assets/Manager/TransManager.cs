
public class TransManager : SingletonMono<TransManager>
{
    public void ChangeScene(string sceneName)
    {
        Transitioner.Instance.TransitionToScene(sceneName);
    }
}
