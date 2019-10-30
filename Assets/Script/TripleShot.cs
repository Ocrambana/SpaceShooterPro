using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    void Update()
    {
        if(transform.childCount < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
