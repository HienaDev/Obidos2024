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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        flying = false;

        timerStanding = Random.Range(timerStoodStill.x, timerStoodStill.y); 
        Debug.Log(timerStanding);

        lerpValue = 0;

        initialPosition = transform.position;


        targetPosition = BirdManager.instance.GetWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - justLanded > timerStanding && !flying)
        {

            
            flying = true;
            animator.SetTrigger("Flying");
            Debug.Log("flying");
            lerpValue = 0;

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, targetPosition.position - transform.position,360, 360));
        }

        if(flying)
        {
            
            if(Vector3.Distance(transform.position, targetPosition.position) < rangeToStopFlyingAnimation)
            {
                animator.SetTrigger("Stop Flying");
                Debug.Log("Not flying");
            }

            if (lerpValue < 1)
            {
                lerpValue += speed * Time.deltaTime;
                lerpValue = Mathf.Min(lerpValue, 1);
                transform.position = Vector3.Lerp(initialPosition, targetPosition.position, lerpValue);
            }
            else
            {
                flying = false;
                timerStanding = Random.Range(timerStoodStill.x, timerStoodStill.y);
                Debug.Log(timerStanding);
                justLanded = Time.time;
                animator.ResetTrigger("Stop Flying");
                initialPosition = transform.position;
                BirdManager.instance.LeaveWaypoints(targetPosition);
                targetPosition = BirdManager.instance.GetWaypoint();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, rangeToStopFlyingAnimation);
    }
}
