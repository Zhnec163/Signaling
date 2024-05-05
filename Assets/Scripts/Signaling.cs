using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private SavingArea _savingArea;
    
    private AudioSource _audioSource;
    private IEnumerator _currentCoroutine;
    private float _timeStep = 0.001F;

    private void Awake()
    {
        _savingArea.Init(TurnOnAlarm, TurnOffAlarm);

        if (TryGetComponent(out AudioSource audioSource))
        {
            _audioSource = audioSource;
        }
    }

    private void TurnOnAlarm()
    {
        float maxAlarmValue = 1;
        StopCurrentCoroutine();
        _currentCoroutine = ChangeVolumeValueTo(_audioSource.volume, maxAlarmValue, _timeStep);
        StartCoroutine(_currentCoroutine);
    }
    
    private void TurnOffAlarm()
    {
        float maxAlarmValue = 0;
        StopCurrentCoroutine();
        _currentCoroutine = ChangeVolumeValueTo(_audioSource.volume, maxAlarmValue, _timeStep);
        StartCoroutine(_currentCoroutine);
    }

    private void StopCurrentCoroutine()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
    }

    private IEnumerator ChangeVolumeValueTo(float current, float target, float timeStep)
    {
        while (Mathf.Approximately(_audioSource.volume, target) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, timeStep);
            yield return new WaitForSeconds(timeStep);
        }
    }
}