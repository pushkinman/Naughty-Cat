using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public static float speed = 4;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -Vector3.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate( -Vector3.forward * speed * Time.deltaTime);
    }
}
