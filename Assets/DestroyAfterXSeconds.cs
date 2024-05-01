using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField] private float x;
    private PlaySounds sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<PlaySounds>();

        sound.PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(x);
        Destroy(gameObject);
    }
}
