using System.Collections;
using UnityEngine;

public class RandomVoices : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float SecondsBetweenIncrement = 20f;
    [SerializeField] private float SecondsBetweenSounds = 8f;

    private AudioSource _audioSource;
    private int index = 0;
    private bool _canPlaySound = true;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(IncrementIndex());
    }

    // PUBLIC

    public void PlayRandomAtIndex()
    {
        if (_canPlaySound)
        {
            int random = index + Random.Range(-2, 3);
            int indexToPlay = Mathf.Clamp(random, 0, clips.Length - 1);
            _audioSource.clip = clips[indexToPlay];
            _audioSource.Play();
            _canPlaySound = false;
            StartCoroutine(PlaySoundsRoutine());
        }
    }

    // PRIVATE

    private IEnumerator PlaySoundsRoutine()
    {
        yield return new WaitForSeconds(SecondsBetweenSounds);
        _canPlaySound = true;
    }

    private IEnumerator IncrementIndex()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(SecondsBetweenIncrement);
            index++;
        }
    }
}
