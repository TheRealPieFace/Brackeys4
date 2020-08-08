using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoffee : MonoBehaviour
{
    public List<GameObject> mugs = new List<GameObject>();
    private PointOfInterest thisPoI;
    private bool active = false;

    private void Start()
    {
        thisPoI = GetComponent<PointOfInterest>();
    }


    private void Update()
    {
        if (thisPoI.interacted && !active)
        {
            foreach (var mug in mugs)
            {
                mug.SetActive(false);
                active = true;
            }
        }
        else if (!thisPoI.interacted && active)
        {
            foreach (var mug in mugs)
            {
                mug.SetActive(true);
                active = false;
            }
        }
    }
}
