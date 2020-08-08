using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEndScenario : MonoBehaviour
{
    private GameManager manager;
    private PointOfInterest thisPoI;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        thisPoI = GetComponent<PointOfInterest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisPoI.interacted && !ended)
        {
            manager.End();
            ended = true;
        }

        if(ended && !thisPoI.interacted)
        {
            ended = false;
        }
    }
}
