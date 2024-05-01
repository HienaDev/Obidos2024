using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{

    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    public AudioSource AudioSource { get { return audioSource; } }
    [SerializeField] private float volume = 1f;
    // Start is called before the first frame update


    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.spatialBlend = 1f;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];

        audioSource.Play();
    }

    public void PlayLoudSound()
    {
        audioSource.spatialBlend = 0.9f;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];

        audioSource.Play();
    }
}
