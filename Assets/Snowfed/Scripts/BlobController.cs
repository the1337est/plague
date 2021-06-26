using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour
{
    Animator animator;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    private bool jumping = false;

    Rigidbody2D rigidbody;
    Vector3 scale;

    public MovementState State;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("Run", true);
            transform.localScale = scale;
            State = MovementState.Right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("Run", true);
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            State = MovementState.Left;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (State == MovementState.Right)
            {
                animator.SetBool("Run", false);
                State = MovementState.Idle;
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (State == MovementState.Left)
            {
                animator.SetBool("Run", false);
                State = MovementState.Idle;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!jumping)
            {
                HandleJump();
            }
        }

        HandleMovement();
    }

    void HandleJump()
    {
        jumping = true;
        rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    void HandleMovement()
    {
        switch (State)
        {
            case MovementState.Idle:
                break;
            case MovementState.Left:
                rigidbody.position += Vector2.left * speed * Time.deltaTime;
                break;
            case MovementState.Right:
                rigidbody.position += Vector2.right * speed * Time.deltaTime;
                break;
            default:
                break;
        }
    }

    public enum MovementState
    {
        Idle,
        Left,
        Right
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumping)
        {
            string tag = collision.collider.tag;
            if (tag == "Ground" || tag == "Platform")
            {
                jumping = false;
                //end jump animation;
            }
        }
    }
}
