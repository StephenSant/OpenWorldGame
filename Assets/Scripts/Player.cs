using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;

    private CharacterController controller;
    private Vector3 motion; 

    void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * groundRayDistance);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        Move(inputH, inputV);
        if (IsGrounded())
        {
            motion.y = 0f;
            if (Input.GetButton("Jump"))
            {
                motion.y = jumpHeight;
            }
        }
        motion.y += gravity * Time.deltaTime;
        controller.Move(motion * Time.deltaTime);
    }


    bool IsGrounded()
    {
        return controller.isGrounded;
    }

    void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        direction = transform.TransformDirection(direction);

        motion.x = direction.x * walkSpeed;
        motion.z = direction.z * walkSpeed;
    }
}
