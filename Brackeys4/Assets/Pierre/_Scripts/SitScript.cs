using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitScript : MonoBehaviour
{
    public bool male = true;
    private TimelineController timeline;
    private bool sitting = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<TimelineController>();
        anim = GetComponentInChildren<Animator>();
        if (!male)
        {
            anim.SetBool("Male", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timeline.lastPoI && !sitting)
        {
            anim.SetBool("Sit", true);
            sitting = true;
        } 
        else if(!timeline.lastPoI && sitting)
        {
            anim.SetBool("Sit", false);
            sitting = false;
        }
    }
}
