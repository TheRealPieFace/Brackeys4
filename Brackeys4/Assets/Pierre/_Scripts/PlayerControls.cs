using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var reversable = FindObjectsOfType<TimelineController>();
            foreach(var character in reversable)
            {
                character.StartRewind();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            var reversable = FindObjectsOfType<TimelineController>();
            foreach (var character in reversable)
            {
                character.StopRewind();
            }
        }
    }
}
