using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingController : MonoBehaviour {

    public float minMoveSpeed = 0.3f;
    public float maxMoveSpeed = 0.7f;

    private float moveSpeed;
    private Vector3 initialDir;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Selector selector;
    private Vector3 lastVelocity;

    private void Start()
    {
        selector = GetComponent<Selector>();

        initialDir = new Vector3(
            Random.Range(
                -1f,
                1f),
            Random.Range(
                -1f,
                1f),
            0);
        

        rb = GetComponent<Rigidbody>();
        

        moveDirection = initialDir.normalized;
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        lastVelocity = moveDirection;
    }

    private void Update()
    {
        float dTime = Time.deltaTime;
        if (!selector.merged)
        {
            //if (rb.drag > 0 && rb.velocity.magnitude <= lastVelocity.magnitude)
            //{
            //    rb.velocity = moveDirection * dTime * moveSpeed; 
            //    rb.drag = 0;
            //}
            //else
            //{
            rb.velocity = (moveDirection * (moveSpeed * 100)) * Time.deltaTime ;
            //}
            lastVelocity = moveDirection * dTime * moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!selector.merged)
        {
            moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal);
        }
    }

}
