using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public float waitTime = 0;
    //public List<TalkingEvents> dialogue;  // TODO
    public bool condition = true;
    public Transform facePoint;
    private Vector3 lookDirection;
    public Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        lookDirection = (facePoint.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);
    }
}
