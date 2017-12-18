using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingController : MonoBehaviour {

    public float minMoveSpeed = 0.3f;
    public float maxMoveSpeed = 0.7f;
    public Vector3 moveDirection;
    public bool slowDown = false;

    private float moveSpeed;
    private Vector3 initialDir;
    private Rigidbody rb;
    private Selector selector;
    private Vector3 originalVelocity;
    private bool firstTime = true;

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

        rb.AddForce(moveDirection * moveSpeed * 100);
        rb.drag = 0;
    }

    private void Update()
    {
        if (firstTime)
        {
            originalVelocity = rb.velocity;
            firstTime = false;
        }
    }

    private void LateUpdate()
    {
        if (!selector.merged
            && rb.velocity.magnitude < originalVelocity.magnitude
            && rb.drag > 0
            && slowDown)
        {
            //rb.MovePosition(transform.position + (moveDirection * Time.deltaTime * moveSpeed));
            rb.drag = 0;
            slowDown = false;
            print("boop");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!selector.merged)
        {
            rb.velocity = Vector3.zero;
            moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal);
            rb.AddForce(moveDirection * moveSpeed * 100);
        }
    }

}
