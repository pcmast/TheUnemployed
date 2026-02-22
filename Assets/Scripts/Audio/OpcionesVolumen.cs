using UnityEngine;
using UnityEngine.Audio;
public class OpcionesVolumen : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMusicVolume(float value)
    {
        // Convertimos de lineal (0 a 1) a decibelios
        mixer.SetFloat("Musica", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("VolumenMúsica", value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("Musica", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Musica", value);
    }

    // Cargar valores guardados
    void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("Musica", 1f);
        float sfxVol = PlayerPrefs.GetFloat("Musica", 1f);

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);
    }
}