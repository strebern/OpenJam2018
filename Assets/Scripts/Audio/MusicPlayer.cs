using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip introClip;
    [SerializeField] private AudioClip crescendoClip;
    [SerializeField] private AudioClip finalLoopClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;

    private AudioSource _musicSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayIntroMusic();
    }

    // PUBLIC

    public void PlayIntroMusic()
    {
        _musicSource.clip = introClip;
        _musicSource.Play();
    }

    public void PlayCrescendo()
    {
        _musicSource.clip = crescendoClip;
        _musicSource.Play();
    }

    public void PlayFinalLoop()
    {
        _musicSource.loop = true;
        _musicSource.clip = finalLoopClip;
        _musicSource.Play();
    }

    public void PlayWinMusic()
    {
        _musicSource.loop = false;
        _musicSource.clip = winClip;
        _musicSource.Play();
    }

    public void PlayLoseMusic()
    {
        _musicSource.loop = false;
        _musicSource.clip = loseClip;
        _musicSource.Play();
    }
}
