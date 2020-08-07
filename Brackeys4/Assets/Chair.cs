using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform sitPos;
    public float yRotation;

    private void Start()
    {
        yRotation = this.transform.localEulerAngles.y;
        Debug.Log(yRotation);
    }

}
