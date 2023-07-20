using UnityEngine;

public class RandomCameraColor : MonoBehaviour
{
    void Start()
    {
        Camera.main.backgroundColor = Random.ColorHSV(0.0f, 1.0f, 0.5f, 0.7f, 0.7f, 0.8f, 1.0f, 1.0f);
    }
}
