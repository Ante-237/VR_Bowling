using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VoidEventSO", menuName = "Events/VoidEventSO")]
public class VoidEventChannel : ScriptableObject
{
    public event Action OnRaiseEvent;

    public void RaiseEvent()
    {
        if(OnRaiseEvent != null)
        {
            OnRaiseEvent.Invoke();
        }
    }
}
