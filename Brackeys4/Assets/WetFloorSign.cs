using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetFloorSign : MonoBehaviour
{
    private bool interactable = false;
    private PlayerControls player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            player.hasWetFloorSign = true;
            gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactable = false;
        }
    }
}
