using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPin : MonoBehaviour
{
    public SoundClipSO soundClipSO;
    public STANDING standstate = STANDING.YES;
    public float ZRotation = 0;
    public float XRotation = 0;
    private AudioSource audioSource;
    private bool runOnce = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            soundClipSO.PlaySound(audioSource);
        }
    }

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
