using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public Animator animator;
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public AnimationEvent turnEvent;
    public int defeated = 0;
    public float deathCount = 5;

    float gravity = -9.81f;
    Vector3 velocity;

    private void Start()
    {
        velocity = Vector3.zero;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (defeated != 1)
        {
            DoMovement();
            DoGravity();
            
        }
        if(defeated == 1)
        {
            Dead();
        }
        if (deathCount < 0 || Input.GetKeyDown(KeyCode.Escape))
        {
           
            End();
        }
        
    }

    void DoMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isMoving", true);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isMoving", false);
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            animator.SetBool("Running", true);
            speed = 6f;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            animator.SetBool("Running", false);
            speed = 3f;
        }
    }


    void DoGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        if( controller.isGrounded == true )
        {
            velocity.y = -2f;
        }

        controller.Move(velocity * Time.deltaTime);

    }
    
    void Death()
    {
        defeated = 1;
    }
    void Dead()
    {
    deathCount = deathCount - Time.deltaTime;
    }
    void End()
    {
        print("Ended");
        Application.Quit();
    }
}
