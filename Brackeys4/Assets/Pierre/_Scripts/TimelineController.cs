using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TimelineController : MonoBehaviour
{
    public enum CharacterState
    {
        Walking,
        Arrived,
        Waiting,
        Idle
    }

    private NavMeshAgent nav;
    public List<PointOfInterest> locations = new List<PointOfInterest>();
    public float turnSpeed = 2;
    private int locationIndex = 0;
    private int timeModifier = 1;
    private bool rewinding = false;
    private bool waiting = false;
    private CharacterState state = CharacterState.Idle;
    private float timer = 0;
    private Animator anim;
    private Quaternion lookRotation;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        nav.updateRotation = false;
        SetLookRotation(locations[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CharacterState.Idle:
                GetLocation();
                break;
            case CharacterState.Walking:
                CheckDestination();
                break;
            case CharacterState.Arrived:
                StartWait();
                break;
            case CharacterState.Waiting:
                //Do Nothing until character's Wait ends
                break;
        }

        if (waiting)
        {
            timer += Time.deltaTime * timeModifier;
            if((!rewinding && timer >= locations[locationIndex].waitTime) || (rewinding && timer < 0))
            {
                waiting = false;
                if(locationIndex < locations.Count)
                {
                    locationIndex += timeModifier;
                    if(locationIndex < 0)
                    {
                        locationIndex = 0;
                    }
                }
                state = CharacterState.Idle;
                timer = 0;
            }
        }

        Rotate();
    }

    private void GetLocation()
    {
        if(locationIndex < locations.Count && locationIndex > -1)
        {
            if (locations[locationIndex].condition)
            {
                if (!rewinding)
                {
                    SetLookRotation(locations[locationIndex].transform.position);
                }
                nav.SetDestination(locations[locationIndex].transform.position);
                state = CharacterState.Walking;
                anim.SetBool("Walking", true);
            }
            else
            {
                if(locationIndex != locations.Count && locationIndex != 0)
                {
                    locationIndex += timeModifier;
                }
            }
        }
    }

    private void CheckDestination()
    {
        float distance = nav.remainingDistance;
        if(distance != Mathf.Infinity && nav.pathStatus == NavMeshPathStatus.PathComplete && nav.remainingDistance == 0)
        {
            lookRotation = locations[locationIndex].lookRotation;
            anim.SetBool("Walking", false);
            state = CharacterState.Arrived;
        }
        if(distance != Mathf.Infinity && rewinding && nav.remainingDistance <= .5)
        {
            lookRotation = locations[locationIndex].lookRotation;
        }
    }

    private void StartWait()
    {
        if (rewinding)
        {
            timer = locations[locationIndex].waitTime;
        }
        waiting = true;
        state = CharacterState.Waiting;
        //begin dialogoue for current location
    }

    public void StartRewind()
    {
        if (!rewinding)
        {
            rewinding = true;
            timeModifier = -1;
            nav.ResetPath();
            if (locationIndex > 0)
            {
                locationIndex -= 1;
            }
            if(state != CharacterState.Waiting)
            {
                state = CharacterState.Idle;
            }
            anim.SetFloat("Time", timeModifier);
        }
    }

    public void StopRewind()
    {
        rewinding = false;
        timeModifier = 1;
        nav.ResetPath();
        //nav.isStopped = true;
        if (state != CharacterState.Waiting)
        {
            state = CharacterState.Idle;
            if (locationIndex < locations.Count)
            {
                locationIndex += 1;
            }
        }
        anim.SetFloat("Time", timeModifier);
    }

    private void SetLookRotation(Vector3 target)
    {
        var lookDirection = (target - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
