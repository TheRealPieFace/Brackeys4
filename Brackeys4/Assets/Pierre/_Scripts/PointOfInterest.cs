using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public Vector3 position;
    public float waitTime = 0;
    //public List<TalkingEvents> dialogue;  // TODO
    public bool condition = true;

    // Start is called before the first frame update
    void Start()
    {
        position = this.transform.position;
    }
}
