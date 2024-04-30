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

    private bool scared;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        flying = false;
        justLanded = Random.Range(0f, 10f);
        timerStanding = Random.Range(timerStoodStill.x, timerStoodStill.y);
        //Debug.Log(gameObject.name + timerStanding);

        lerpValue = 0;

        initialPosition = transform.position;

        scared = false;

        targetPosition = BirdManager.instance.GetWaypoint();
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - justLanded > timerStanding && !flying)
        {


            flying = true;
            animator.SetTrigger("Flying");
            Debug.Log(gameObject.name + "flying");
            lerpValue = 0;

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, targetPosition.position - transform.position, 360, 360));
        }

        if (flying)
        {

            if (Vector3.Distance(transform.position, targetPosition.position) < rangeToStopFlyingAnimation)
            {
                animator.SetTrigger("Stop Flying");
                Debug.Log(gameObject.name + "Not flying");
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
                //Debug.Log(gameObject.name + timerStanding);
                justLanded = Time.time;
                lerpValue = 0;

                if (!scared)
                {

                    initialPosition = transform.position;
                    Transform tempTransform = targetPosition;
                    targetPosition = BirdManager.instance.GetWaypoint();
                    Debug.Log(gameObject.name + " was at " + tempTransform.gameObject.name + " wants to go to " + targetPosition.gameObject.name);
                    BirdManager.instance.LeaveWaypoints(tempTransform);
                    
                    animator.ResetTrigger("Stop Flying");
                    Debug.Log(gameObject.name + "remove trigger stop flying");
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
        Debug.Log(gameObject.name + "flying scared");
        lerpValue = 0;
        initialPosition = transform.position;
        targetPosition = BirdManager.instance.scarePoint;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, targetPosition.position - transform.position, 360, 360));
    }
}
