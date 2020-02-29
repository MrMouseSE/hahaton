using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainMusicController : MonoBehaviour
{
    public List<AudioClip> _audioClips;
    public AudioSource _audioSource;

    private float timeCounter;
    private float trackLenght;
    private void Awake()
    {
        timeCounter = 0;
        _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Count)];
        trackLenght = _audioSource.clip.length;
        _audioSource.Play();
    }


    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > trackLenght)
        {
            timeCounter = 0;
            _audioSource.Stop();
            _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Count)];
            trackLenght = _audioSource.clip.length;
            _audioSource.Play();
        }
    }
}
