using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private Vector3 _direction = Vector3.up;

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        if(Mathf.Abs(transform.position.y) > 8f)
        {
            Destroy(this.gameObject);
        }
    }
}
