using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OpcionesVolumen : MonoBehaviour
{

    public AudioSource[] musicSources;
    public Slider slider;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        if (musicSources.Length > 0) slider.value = musicSources[0].volume;

        slider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMusicVolume(float value)
    {
        foreach (var source in musicSources)
        {
            source.volume = value;
        }
        Debug.Log("Volumen actual para todos: " + value);
    }
}