using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleInteraction : MonoBehaviour
{
    private bool interactable = false;
    public List<PointOfInterest> newPath = new List<PointOfInterest>();
    public GameObject sign;
    private PlayerControls player;
    private PointOfInterest thisPoI;
    private GameManager manager;
    public GameObject prompt;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
        thisPoI = GetComponent<PointOfInterest>();
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            sign.SetActive(true);
            player.hasWetFloorSign = false;
            thisPoI.condition = false;
            foreach(var point in newPath)
            {
                point.condition = true;
            }
            manager.totalFixes++;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponent<PlayerControls>().hasWetFloorSign)
            {
                interactable = true;
                prompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactable = false;
            prompt.SetActive(false);
        }
    }
}
