using UnityEngine;

public class PC : MonoBehaviour
{
    public GameObject windowApp1;
    public GameObject windowApp2;

    public void OpenApp1()
    {
        windowApp1.SetActive(true);
    }

    public void CloseApp1()
    {
        windowApp1.SetActive(false);
    }

    public void OpenApp2()
    {
        windowApp2.SetActive(true);
    }

    public void CloseApp2()
    {
        windowApp2.SetActive(false);
    }
}
