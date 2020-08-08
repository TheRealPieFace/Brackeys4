﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloatingText : MonoBehaviour
{
    // Start is called before the first frame update

    public float DestroyTime = 3f;
    public Vector3 Offset = new Vector3(0, 0, 0);
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += Offset;
    }

  
}