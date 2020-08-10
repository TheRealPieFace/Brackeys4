using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongOrder : MonoBehaviour
{
    public PointOfInterest thisPoI;
    private bool active = false;
    private CoffeeShopManager cManager;
    public DialogueTrigger dialogue;

    private void Start()
    {
        cManager = FindObjectOfType<CoffeeShopManager>();
        thisPoI = GetComponent<PointOfInterest>();
    }


    private void Update()
    {
        if (thisPoI.talking && !active)
        {
            if (!cManager.mugSwitched)
            {
                dialogue.TriggerDialogue();
            }
            active = true;
        }
        else if (!thisPoI.talking && active)
        {
            active = false;
        }
    }
}
