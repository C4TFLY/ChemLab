using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingController : MonoBehaviour {

    private float moveSpeed;
    private Vector3 initialDir;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float deltaTime;

    private void Start()
    {

#region Vectors

        initialDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

        #endregion

#region Components

        rb = GetComponent<Rigidbody>();

        #endregion

#region Variable assignment

        moveDirection = initialDir;
        deltaTime = GameManager.deltaTime;
        moveSpeed = Random.Range(0.3f, 0.7f);

#endregion

    }

    private void FixedUpdate()
    {
        deltaTime = GameManager.deltaTime;
        rb.MovePosition(transform.position + moveDirection * deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        moveDirection = collision.contacts[0].normal;
        moveDirection = Quaternion.AngleAxis(Random.Range(-70f, 70f), Vector3.forward) * moveDirection;
    }

}
