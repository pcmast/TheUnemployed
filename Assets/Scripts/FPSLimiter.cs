using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public class FPSLimiter : MonoBehaviour
{
    public static int fps = 144;
    void Start()
    {
        Application.targetFrameRate = fps;
        QualitySettings.vSyncCount = 0;
    }
}
}
