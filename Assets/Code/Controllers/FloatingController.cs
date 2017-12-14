using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingController : MonoBehaviour {

    public float minMoveSpeed = 0.3f;
    public float maxMoveSpeed = 0.7f;
    public Vector3 moveDirection;

    private float moveSpeed;
    private Vector3 initialDir;
    private Rigidbody rb;
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
        if (rb.velocity.magnitude < 0.01)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
        }
        if (!selector.merged)
        {
            rb.MovePosition(transform.position + (moveDirection * Time.deltaTime * moveSpeed));
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
