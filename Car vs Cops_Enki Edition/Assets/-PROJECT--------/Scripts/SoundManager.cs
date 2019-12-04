using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SoundManager>();
            return _instance;
        }
    }
    private static SoundManager _instance;

    public AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip collision;
    [SerializeField] private AudioClip coinPickup;
    [SerializeField] private AudioClip uiButton;



    [SerializeField] private AudioMixer mixer;


    public void Explosion() { audioSource.PlayOneShot(explosion); }   
    public void Collision() { audioSource.PlayOneShot(collision); }
    public void CoinPickup() { audioSource.PlayOneShot(coinPickup); }
    public void UiButton() { audioSource.PlayOneShot(uiButton); }




    //All of this is ready, but I don't have time to do a proper option menu (it would take a few hours)

    public void MuteMusic()
    {
        mixer.SetFloat("music", 0);
    }

    public void UnMuteMusic()
    {
        mixer.SetFloat("music", 1);
    }

    public void MuteSFX()
    {
        mixer.SetFloat("SFX", 0);
    }

    public void UnMuteSFX()
    {
        mixer.SetFloat("SFX", 1);
    }
}
