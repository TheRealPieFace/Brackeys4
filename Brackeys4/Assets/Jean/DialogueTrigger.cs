using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private PointOfInterest thisPoI;
    private bool speaking = false;
    public bool external = false;

    private void Start()
    {
        thisPoI = GetComponent<PointOfInterest>();
    }

    private void Update()
    {
        if (thisPoI.talking && !speaking && !external)
        {
            speaking = true;
            TriggerDialogue();
        }
        if (speaking && !thisPoI.talking)
        {
            speaking = false;
        }
    }

    public void TriggerDialogue()
    {
        thisPoI.owner.GetComponent<TextBubble>().ShowTextBubble(dialogue);
   
    }
}