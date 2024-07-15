using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPin : MonoBehaviour
{
    public Vector3 WorldUp = Vector3.up;
    public STANDING standstate = STANDING.YES;
    public float ZRotation = 0;
    public float XRotation = 0;

    private bool runOnce = false;

    private void Update()
    {
        if (Time.frameCount % 200 == 0)
        {
            if(!runOnce)
            {
                Quaternion quaternion = transform.rotation;
                ZRotation = quaternion.eulerAngles.z;
                XRotation = quaternion.eulerAngles.x;
                if ((ZRotation > 30 && ZRotation < 340) || (XRotation > 30 && XRotation < 340))
                {
                    standstate = STANDING.NO;
                    runOnce = true;
                }
            }
            
        }
    }

    
}
