using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSeats : MonoBehaviour
{
    private PointOfInterest thisPoI;
    private bool activated = false;
    private CoffeeShopManager cManager;

    private void Start()
    {
        thisPoI = GetComponent<PointOfInterest>();
        cManager = FindObjectOfType<CoffeeShopManager>();
    }

    private void Update()
    {
        if(!activated && thisPoI.interacted && cManager.sittingInSpot)
        {
            cManager.TogglePath2();
            activated = true;
        }

        if(activated && !thisPoI.interacted)
        {
            cManager.TogglePath1();
            activated = false;
        }
    }
}
