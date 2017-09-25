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

    private void Start()
    {
        initialDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        

        rb = GetComponent<Rigidbody>();
        

        moveDirection = initialDir;
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal);
    }

}
