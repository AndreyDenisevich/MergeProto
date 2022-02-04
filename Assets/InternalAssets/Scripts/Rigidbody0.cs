using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigidbody0 : MonoBehaviour
{
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = Vector3.zero;
    }
}
