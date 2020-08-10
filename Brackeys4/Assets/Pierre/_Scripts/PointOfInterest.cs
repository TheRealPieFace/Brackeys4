using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public bool interactable = false;
    public bool interacted = false;
    public bool talking = false;
    public float waitTime = 0;
    //public List<TalkingEvents> dialogue;  // TODO
    public bool condition = true;
    public Transform facePoint;
    private Vector3 lookDirection;
    public Quaternion lookRotation;
    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        lookDirection = (facePoint.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);
    }

    public void Interact(GameObject interactee)
    {
        if (interactable)
        {
            owner = interactee;
            interacted = !interacted;
        }
    }
}
