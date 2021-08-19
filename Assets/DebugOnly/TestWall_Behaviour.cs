using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWall_Behaviour : MonoBehaviour
{
    private Rigidbody2D thisBody;
    public float speed;

    void Start()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        thisBody.velocity = -transform.up * speed;
    }


}
