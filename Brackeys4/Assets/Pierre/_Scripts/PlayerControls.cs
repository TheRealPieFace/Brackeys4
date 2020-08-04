using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcess;
    private ChromaticAberration abberation;
    private bool rewind = false;
    [SerializeField] private float abberationIntensity = .5f;
    [SerializeField] private float abberationSpeed = .1f;

    // Start is called before the first frame update
    void Start()
    {
        postProcess.profile.TryGetSettings(out abberation);
        Debug.Log(abberation != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var reversable = FindObjectsOfType<TimelineController>();
            foreach(var character in reversable)
            {
                rewind = true;
                character.StartRewind();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            var reversable = FindObjectsOfType<TimelineController>();
            foreach (var character in reversable)
            {
                rewind = false;
                character.StopRewind();
            }
        }

        if(rewind && abberation.intensity.value < abberationIntensity)
        {
            abberation.intensity.value += Time.deltaTime * abberationSpeed;
        } 
        else if (!rewind && abberation.intensity.value > 0)
        {
            abberation.intensity.value -= Time.deltaTime * abberationSpeed;
        }

    }
}
