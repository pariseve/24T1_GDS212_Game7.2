using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource AnomolySource;

    [Header("----- Audio Clip -----")]
    public AudioClip background;
    public AudioClip pianoSound;

    public AudioClip clickSound;
    public AudioClip buttonSound;

    private void Start()
    {
        backgroundSource.clip = background;
        backgroundSource.Play();
    }

    public void PlayAnomoly(AudioClip clip)
    {
        AnomolySource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void clickSFX()
    {
        SFXSource.PlayOneShot(buttonSound);
    }


}