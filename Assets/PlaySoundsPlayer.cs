using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsCam;
    [SerializeField] private AudioClip[] audioClipsShoot;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
    }

    public void PlaySoundCam()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.clip = audioClipsCam[Random.Range(0, audioClipsCam.Length)];

        audioSource.Play();
    }

    public void PlaySoundShoot()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.clip = audioClipsShoot[Random.Range(0, audioClipsShoot.Length)];

        audioSource.Play();
    }
}
