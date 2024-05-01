using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ScareBirds : MonoBehaviour
{

    [SerializeField] private float timeToScare;
    [SerializeField] private float sphereRadius;
    public bool Sitting { get; private set; }
    private float justSat;

    [SerializeField] private LayerMask birdLayer;
    private Animator animator;

    private GameObject currentBird;
    private float defaultSpeed;

    private Shootable shootScript;
    private PlaySounds sounds;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Sitting = false;
        justSat = Time.time;
        animator.SetBool("Walking", true);

        sounds = GetComponent<PlaySounds>();

        shootScript = GetComponent<Shootable>();

        defaultSpeed = GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(shootScript.BadDog);
        if (Time.time - justSat > timeToScare && Sitting)
        {
            currentBird.gameObject.GetComponent<BirdLogic>().ScareMe();
            animator.SetBool("Walking", true);
            Sitting = false;
            shootScript.BadDog = false;
            sounds.PlaySound();
            ScoreManager.instance.AddScore(-20);
            GetComponent<NavMeshAgent>().speed = defaultSpeed;
        }
        //Debug.Log("sitting: " + Sitting);
    }

    private void FixedUpdate()
    {
        Collider[] tempBird = Physics.OverlapSphere(transform.position, sphereRadius, birdLayer);

        if (tempBird.Length > 0 && !Sitting)
        {
            currentBird = tempBird[0].gameObject;
            animator.SetBool("Walking", false);
            justSat = Time.time;
            shootScript.BadDog = true;
            Sitting = true;
            GetComponent<NavMeshAgent>().speed = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
