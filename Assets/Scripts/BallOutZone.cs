using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOutZone : MonoBehaviour
{
    public SettingSO settings;
    public VoidEventChannel OutRangeEvent;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("ball"))
        {
            Destroy(other.gameObject, 8f);
            OutRangeEvent.RaiseEvent();
            // update score
            // 
        }
    }

}
