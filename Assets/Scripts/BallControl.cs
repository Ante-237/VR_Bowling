using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallControl : MonoBehaviour
{
    public bool hasTriggered = false;
    public SettingSO settings;
    public SoundClipSO rollingSound;
    public SoundClipSO dropSound;
    public VoidEventChannel GameStatusUpdateEvent;
    public VoidEventChannel BallOutEvent;


    private AudioSource audioSource;
    private Rigidbody rb;
    private bool runOnce = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.frameCount % 500 == 0)
        {
            if (rb.velocity.magnitude < 0.4)
            {
                audioSource.Stop();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("duckpin"))
        {
            StartCoroutine(UpdateCurrentStatus(6.0f));
        }

        if (collision.gameObject.CompareTag("floor"))
        {
            if (!runOnce)
            {
                dropSound.PlaySound(audioSource);
                rollingSound.PlayContinous(audioSource);
                runOnce = true;
            }
        }

    }

    IEnumerator UpdateCurrentStatus(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        if (!hasTriggered)
        {
            Destroy(gameObject);
            settings.balls -= 1;
            GameStatusUpdateEvent.RaiseEvent();
            if (settings.balls < 1)
            {
                BallOutEvent.RaiseEvent();
            }

        }
    }
}
