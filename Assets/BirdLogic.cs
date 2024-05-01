using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLogic : MonoBehaviour
{

    private float timerStanding;
    private float justTriggered;
    private bool flying;

    [SerializeField] private Vector2 timerStoodStill;
    [SerializeField] private float rangeToStopFlyingAnimation;
    private float justLanded;

    private Animator animator;

    [SerializeField] private float speed;
    private float lerpValue;
    private Vector3 initialPosition;
    private Transform targetPosition;

    [SerializeField] private string species;
    public string Species {  get { return species; } }

    private bool scared;

    [SerializeField] private Vector2 soundCooldown;
    private float soundCooldownValue;
    private PlaySounds sounds;
    private float justSounded;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        flying = false;
        justLanded = Random.Range(0f, 10f);
        timerStanding = Random.Range(timerStoodStill.x, timerStoodStill.y);
        ////Debug.Log(gameObject.name + timerStanding);

        lerpValue = 0;

        initialPosition = transform.position;

        sounds = GetComponent<PlaySounds>();

        justSounded = Time.time;
        soundCooldownValue = Random.Range(soundCooldown.x, soundCooldown.y);
        sounds.AudioSource.volume = 0.8f;

        scared = false;

        targetPosition = BirdManager.instance.GetWaypoint();

        if (species == "green") GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
        if (species == "red") GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        if (species == "blue") GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
        if (species == "yellow") GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
        if (species == "cyan") GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time - justSounded > soundCooldownValue)
        {
            justSounded = Time.time;
            soundCooldownValue = Random.Range(soundCooldown.x, soundCooldown.y);
            sounds.PlaySound();
        }

        if (Time.time - justLanded > timerStanding && !flying)
        {


            flying = true;
            animator.SetTrigger("Flying");
            //Debug.Log(gameObject.name + "flying");
            lerpValue = 0;

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, targetPosition.position - transform.position, 360, 360));
        }

        if (flying)
        {

            if (Vector3.Distance(transform.position, targetPosition.position) < rangeToStopFlyingAnimation)
            {
                animator.SetTrigger("Stop Flying");
                
                //Debug.Log(gameObject.name + "Not flying");
            }

            if (lerpValue < 1)
            {
                lerpValue += (speed / Vector3.Distance(initialPosition, targetPosition.position)) * Time.deltaTime;
                lerpValue = Mathf.Min(lerpValue, 1);
                transform.position = Vector3.Lerp(initialPosition, targetPosition.position, lerpValue);
            }
            else
            {
                flying = false;
                timerStanding = Random.Range(timerStoodStill.x, timerStoodStill.y);
                ////Debug.Log(gameObject.name + timerStanding);
                justLanded = Time.time;
                lerpValue = 0;

                if (!scared)
                {

                    initialPosition = transform.position;
                    Transform tempTransform = targetPosition;
                    targetPosition = BirdManager.instance.GetWaypoint();
                    //Debug.Log(gameObject.name + " was at " + tempTransform.gameObject.name + " wants to go to " + targetPosition.gameObject.name);
                    BirdManager.instance.LeaveWaypoints(tempTransform);
                    
                    animator.ResetTrigger("Stop Flying");
                    //Debug.Log(gameObject.name + "remove trigger stop flying");

                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, rangeToStopFlyingAnimation);
    }

    [Button]
    public void ScareMe()
    {
        scared = true;
        flying = true;
        animator.SetTrigger("Flying");
        //Debug.Log(gameObject.name + "flying scared");
        lerpValue = 0;
        initialPosition = transform.position;
        targetPosition = BirdManager.instance.scarePoint;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, targetPosition.position - transform.position, 360, 360));
    }
}
