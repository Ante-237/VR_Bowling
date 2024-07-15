using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallOutZone : MonoBehaviour
{
    public SettingSO settings;
    public VoidEventChannel OutRangeEvent;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("ball"))
        {
            if(other.transform.TryGetComponent(out BallControl control))
            {
                if (!control.hasTriggered)
                {
                    Invoke(nameof(CallEvent), settings.TimeBtwGameChecks);
                    Destroy(other.gameObject, settings.TimeBtwGameChecks  + 1);
                    control.hasTriggered = true;
                }
            }
            
        }
    }

    private void CallEvent()
    {
        OutRangeEvent.RaiseEvent();
    }

}
