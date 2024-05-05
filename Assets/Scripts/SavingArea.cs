using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SavingArea : MonoBehaviour
{
    private Action _thiefInside;
    private Action _thiefOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            _thiefInside.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            _thiefOut.Invoke();
        }
    }

    private void OnDestroy()
    {
        _thiefInside = null;
        _thiefOut = null;
    }

    public void Init(Action turnOnAlarm, Action turnOffAlarm)
    {
        _thiefInside += turnOnAlarm;
        _thiefOut += turnOffAlarm;
    }
}
