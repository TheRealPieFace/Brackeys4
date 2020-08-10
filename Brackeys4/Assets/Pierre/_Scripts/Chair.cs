using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform sitPos;
    public float yRotation;
    public bool activation = false;
    public GameObject prompt;

    private void Start()
    {
        yRotation = this.transform.localEulerAngles.y;
    }

    public void ShowPrompt()
    {
        prompt.SetActive(true);
    }

    public void HidePrompt()
    {
        prompt.SetActive(false);
    }

}
