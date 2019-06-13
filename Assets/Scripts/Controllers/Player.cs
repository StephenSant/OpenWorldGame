using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;

    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;
    [Header("Dash")]
    public float dashDuration = .5f;
    bool dashing = false;

    private CharacterController controller;
    private Vector3 motion;
    private bool isJumping;
    private float currentJumpHeight, currentSpeed;
    private bool usingTime = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        currentJumpHeight = jumpHeight;
    }

    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        #region Old Code
        //bool inputRun = Input.GetKeyDown(KeyCode.LeftShift);
        //bool inputWalk = Input.GetKeyUp(KeyCode.LeftShift);
        /*if (inputWalk)
                {
                    currentSpeed = walkSpeed;
                }
                */
        #endregion
        bool inputRun = Input.GetKey(KeyCode.LeftShift);

        bool inputJump = Input.GetButton("Jump");


        if (!dashing)
        {
            if (inputRun)
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }
        Move(inputH, inputV);

        if (controller.isGrounded)
        {
            motion.y = 0f;
            if (inputJump)
            {
                Jump(jumpHeight);
            }
            if (isJumping)
            {
                motion.y = currentJumpHeight;
                isJumping = false;
            }
        }
        motion.y += gravity * Time.deltaTime;
        controller.Move(motion * Time.deltaTime);
    }


    public void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        direction = transform.TransformDirection(direction);

        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }

    public IEnumerator CountDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        dashing = false;
    }

    public void Jump(float height)
    {
        isJumping = true;
        currentJumpHeight = height;
    }

    public void AbilityTimer(float time)
    {
        StartCoroutine(CountDown(time));
    }

    public void Dash(float speed)
    {
        dashing = true;
        currentSpeed = speed;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= 5;
        }
    }
}
