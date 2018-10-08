using System.Collections;
using UnityEngine;

public class RandomVoices : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float SecondsBetweenIncrement = 30f;
    [SerializeField] private float SecondsBetweenSounds = 10f;

    private AudioSource _audioSource;
    private int index = 0;
    private bool _canPlaySound = true;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(PlaySoundsRoutine());
        StartCoroutine(IncrementIndex());
    }

    private void PlayRandomAtIndex()
    {
        if (_canPlaySound)
        {
            int indexToPlay = Mathf.Clamp(index + Random.Range(-1, 1), 0, clips.Length - 1);
            _audioSource.clip = clips[indexToPlay];
            _audioSource.Play();
        }
    }

    private IEnumerator PlaySoundsRoutine()
    {
        _canPlaySound = false;
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
