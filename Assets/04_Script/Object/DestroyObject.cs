using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }
}
