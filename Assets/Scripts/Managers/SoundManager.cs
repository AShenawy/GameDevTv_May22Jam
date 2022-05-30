using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip bgmTrack;

    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioSource bgmSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bgmSource.clip = bgmTrack;
        bgmSource.Play();
    }

    public void PlayeSFX(AudioClip sfx, float volume = 1f)
    {
        sfxSource.PlayOneShot(sfx, volume);
    }
}