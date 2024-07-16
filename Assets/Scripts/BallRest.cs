using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRest : MonoBehaviour
{
    public SettingSO settings;
    public VoidEventChannel BallCountCaller;

    private void Start()
    {
       // settings.balls = 6;
    }

    //  check the number of balls everytime a hand gets through


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            if(settings.balls < 1)
            {
                BallCountCaller.RaiseEvent();
            }
        }
    }
}
