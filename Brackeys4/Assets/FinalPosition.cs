using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPosition : MonoBehaviour
{
    private PointOfInterest thisPoI;
    private CoffeeShopManager cManager;
    private GameManager manager;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        thisPoI = GetComponent<PointOfInterest>();
        cManager = FindObjectOfType<CoffeeShopManager>();
        manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (thisPoI.interacted && !ended)
        {
            if (cManager.mugSwitched)
            {
                manager.Win();
            }
            else
            {
                manager.End();
                ended = true;
            }
            
        }

        if (ended && !thisPoI.interacted)
        {
            ended = false;
        }
    }
}
