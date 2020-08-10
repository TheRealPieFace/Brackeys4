using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCoffee : MonoBehaviour
{
    public List<GameObject> mugs = new List<GameObject>();
    private PointOfInterest thisPoI;
    private bool active = false;
    private CoffeeShopManager cManager;

    private void Start()
    {
        cManager = FindObjectOfType<CoffeeShopManager>();
        thisPoI = GetComponent<PointOfInterest>();
    }


    private void Update()
    {
        if (thisPoI.interacted && !active)
        {
            foreach(var mug in mugs)
            {
                mug.SetActive(true);
                active = true;
            }
        }
        else if(!thisPoI.interacted && active)
        {
            foreach(var mug in mugs)
            {
                mug.SetActive(false);
                active = false;
                cManager.mugSwitched = false;
            }
        }
    }
}
