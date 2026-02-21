using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour
{
    public RectTransform crosshair;

    void Update()
    {
        // Centrar la mirilla en el medio de la pantalla
        crosshair.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }
}
