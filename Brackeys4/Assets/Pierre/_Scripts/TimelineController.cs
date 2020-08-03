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
    public int locationIndex = 0;
    private int timeModifier = 1;
    public bool rewinding = false;
    public bool waiting = false;
    public CharacterState state = CharacterState.Idle;
    public float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
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
    }

    private void GetLocation()
    {
        if(locationIndex < locations.Count && locationIndex > -1)
        {
            if (locations[locationIndex].condition)
            {
                nav.SetDestination(locations[locationIndex].position);
                Debug.Log(nav.destination);
                state = CharacterState.Walking;
                //set anim to walking
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
            state = CharacterState.Arrived;
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
            
            //set anim speed to timeModifier
        }
    }

    public void StopRewind()
    {
        rewinding = false;
        timeModifier = 1;
        nav.ResetPath();
        //nav.isStopped = true;
        if (locationIndex < locations.Count)
        {
            locationIndex += 1;
        }
        if (state != CharacterState.Waiting)
        {
            state = CharacterState.Idle;
        }
        //stop all coroutines
        //set anim speed to timeModifier;
    }
}
