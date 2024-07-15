using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPin : MonoBehaviour
{
    public Vector3 WorldUp = Vector3.up;
    public STANDING standstate;

    public void Start()
    {
        standstate = STANDING.YES;
    }

    private void Update()
    {
        if(Time.frameCount % 400 == 0)
        {
            Debug.Log("Falling reverse : " + Vector3.Angle(transform.up, WorldUp) * Mathf.Rad2Deg);
            Debug.Log("Angle Btw : " + Vector3.Angle(transform.up, WorldUp));
            Debug.Log("Up Local Vector : " + transform.up);
          
            if (Vector3.Dot(transform.up, WorldUp) > 0.1)
            {
                standstate = STANDING.NO;
            }
        }
    }
}
