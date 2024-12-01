using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip dayClip;
    [SerializeField] private AudioClip nightClip;


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void AudioManager_OnNightStarted()
    {
        audioSource.PlayOneShot(nightClip);
    }

    private void AudioManager_OnDayStarted()
    {
        audioSource.PlayOneShot(dayClip);
    }


    private void OnEnable()
    {
        CycleManager.OnDayStarted += AudioManager_OnDayStarted;
        CycleManager.OnNightStarted += AudioManager_OnNightStarted;
    }

    private void OnDisable()
    {
        CycleManager.OnNightStarted -= AudioManager_OnNightStarted;
        CycleManager.OnNightStarted -= AudioManager_OnNightStarted;
    }
}
