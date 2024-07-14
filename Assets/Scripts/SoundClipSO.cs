using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundClipSO", menuName = "Sound/SoundClipSO")]
public class SoundClipSO : ScriptableObject
{
    public List<AudioClip> clip;

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(clip[Random.Range(0, clip.Count)]);
    }
}
