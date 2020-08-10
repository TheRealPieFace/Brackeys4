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

    //private NavMeshAgent nav;
    public List<PointOfInterest> locations = new List<PointOfInterest>();
    public float turnSpeed = 2;
    public float movementSpeed = 2;
    [SerializeField] private int locationIndex = 0;
    private int timeModifier = 1;
    public bool rewinding = false;
    private bool waiting = false;
    private bool walking = false;
    public CharacterState state = CharacterState.Idle;
    private float timer = 0;
    private Animator anim;
    private Quaternion lookRotation;
    private Vector3 destination;
    public bool lastPoI = false;


    // Start is called before the first frame update
    void Start()
    {
        //nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //nav.updateRotation = false;
        SetLookRotation(locations[0].transform.position, transform.position);
        SetDestination(locations[locationIndex].transform.position);
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
            if((!lastPoI && !rewinding && timer >= locations[locationIndex].waitTime) || (rewinding && timer < 0))
            {
                waiting = false;
                if(locationIndex < locations.Count - 1)
                {
                    if (rewinding && locations[locationIndex].GetComponent<PointOfInterest>().interacted)
                    {
                        locations[locationIndex].GetComponent<PointOfInterest>().interacted = false;
                    }
                    locations[locationIndex].talking = false;
                    locationIndex += timeModifier;
                    if(locationIndex < 0)
                    {
                        locationIndex = 0;
                    }
                } else if(lastPoI && rewinding)
                {
                    locationIndex += timeModifier;
                    lastPoI = false;
                }
                state = CharacterState.Idle;
                timer = 0;
            }
        }

        Rotate();
        if(walking)
        {
            Translate();
        }


        
    }

    private void GetLocation()
    {
        if(rewinding && locationIndex == 0 && RemainingDistance(locations[0].transform.position) <= 0.1f && RemainingDistance(locations[0].transform.position) >= -0.1f)
        {
            return;
        }

        if(locationIndex == locations.Count - 1 && RemainingDistance(locations[locations.Count - 1].transform.position) <= 0.1f && RemainingDistance(locations[locations.Count - 1].transform.position) >= -0.1f)
        {
            lastPoI = true;
            waiting = true;
            state = CharacterState.Waiting;
        }

        if(locationIndex < locations.Count && locationIndex > -1)
        {
            if (locations[locationIndex].condition)
            {
                if (!rewinding)
                {
                    SetLookRotation(locations[locationIndex].transform.position, transform.position);
                }
                else if(locationIndex < locations.Count)
                {
                    SetLookRotation(locations[locationIndex + 1].transform.position, locations[locationIndex].transform.position);
                }
                else if (locationIndex > 0)
                {
                    SetLookRotation(locations[locationIndex - 1].transform.position, locations[locationIndex].transform.position);
                }
                SetDestination(locations[locationIndex].transform.position);
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
        if(RemainingDistance() <= 0.1f && RemainingDistance() >= -0.1f)
        {
            //lookRotation = locations[locationIndex].lookRotation;
            anim.SetBool("Walking", false);
            state = CharacterState.Arrived;
        }
        if (RemainingDistance() <= 2)
        {
            lookRotation = locations[locationIndex].lookRotation;
        }
    }

    private void StartWait()
    {
        walking = false;

        locations[locationIndex].talking = true;

        if (!rewinding)
        {
            locations[locationIndex].Interact(this.gameObject);
        }
        
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
            //nav.ResetPath();
            
            if(state != CharacterState.Waiting)
            {
                if (locationIndex > 0)
                {
                    locationIndex -= 1;
                }
                state = CharacterState.Idle;
            }
            else
            {
                
            }


            anim.SetFloat("Time", timeModifier);
        }
    }

    public void StopRewind()
    {
        rewinding = false;
        timeModifier = 1;
        //nav.ResetPath();
        if (state != CharacterState.Waiting)
        {
            if (locationIndex < locations.Count && locationIndex != 0)
            {
                locationIndex += 1; 
            } 
            else if(locationIndex == 0 && state == CharacterState.Walking)
            {
                locationIndex += 1;
            }
            state = CharacterState.Idle;
        }
        anim.SetFloat("Time", timeModifier);
    }

    private void SetLookRotation(Vector3 target, Vector3 position)
    {
        var lookDirection = (target - position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void SetDestination(Vector3 target)
    {
        destination = new Vector3(target.x, transform.position.y, target.z);
        walking = true;
    }

    private void Translate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
    }

    private float RemainingDistance()
    {
        return Mathf.Sqrt(Mathf.Pow(destination.x - transform.position.x, 2) + Mathf.Pow(destination.z - transform.position.z, 2));
    }
    private float RemainingDistance(Vector3 location)
    {
        return Mathf.Sqrt(Mathf.Pow(location.x - transform.position.x, 2) + Mathf.Pow(location.z - transform.position.z, 2));
    }
}
