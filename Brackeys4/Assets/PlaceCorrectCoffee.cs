using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCorrectCoffee : MonoBehaviour
{
    public List<GameObject> correctMugs = new List<GameObject>();
    public List<GameObject> incorrectMugs = new List<GameObject>();
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
            if (cManager.mugSwitched)
            {
                foreach (var mug in correctMugs)
                {
                    mug.SetActive(true);
                    active = true;
                }
            }
            else
            {
                foreach (var mug in incorrectMugs)
                {
                    mug.SetActive(true);
                    active = true;
                }
            }
            
        }
        else if (!thisPoI.interacted && active)
        {
            if (cManager.mugSwitched)
            {
                foreach (var mug in correctMugs)
                {
                    mug.SetActive(false);
                    active = false;
                }
            }
            else
            {
                foreach (var mug in incorrectMugs)
                {
                    mug.SetActive(false);
                    active = false;
                }
            }
        }
    }
}
