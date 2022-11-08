using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBaseMov : MonoBehaviour
{

    // PHYSICS COMPONENTS AND OFFSETS
    private Rigidbody2D rb;
    private BoxCollider2D bodyCollider;
    private float extraColliderHeight = .02f;
    [SerializeField] private LayerMask ground;

    //WALK PARAMETERS
    [Range(0, 10)]
    public float walkSpeed;
    private float walkDirection;

    // INPUT VARIABLES
    private GameObject inputListener;
    private string jumpKey;

    // ANIMATION VARIABLES
    private Animator animator;

    //JUMP INPUTS
    public bool jumpPress;
    public bool jumpRelease;

    //JUMP PARAMETERS
    [Range(0, 10)]
    public float jumpForce;
    [Range(0, 300)]
    public int jumpStopForce;
    public float maxYVelocity;
    public bool CanJump;
    [Range(0,1)]
    public float bufferJumpDistance;

    void Start()
    {
        inputListener = GameObject.Find("Input Listener");
        jumpKey = inputListener.GetComponent<buttonInputs>().Jump;

        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {   /*
        // DEBUGGING THE GROUND CHECK COLLIDER AND RAY
        Color debugBoxColor;
        Color debugRayColor;
        if (IsGrounded())
        {
            debugBoxColor = Color.green;
            debugRayColor = Color.green;
        }
        else
        {
            debugBoxColor = Color.red;
            if (CanBufferJump(bufferJumpDistance))
            {
                debugRayColor = Color.green;
            }
            else
            {
                debugRayColor = Color.red;
            }
        }
        Debug.DrawRay(bodyCollider.bounds.center + new Vector3(bodyCollider.bounds.extents.x, 0), Vector2.down * (bodyCollider.bounds.extents.y + extraColliderHeight), debugBoxColor);
        Debug.DrawRay(bodyCollider.bounds.center - new Vector3(bodyCollider.bounds.extents.x, 0), Vector2.down * (bodyCollider.bounds.extents.y + extraColliderHeight), debugBoxColor);
        Debug.DrawRay(bodyCollider.bounds.center - new Vector3(bodyCollider.bounds.extents.x, bodyCollider.bounds.extents.y + extraColliderHeight), Vector2.right * (bodyCollider.bounds.size.x), debugBoxColor);
        Debug.DrawRay(bodyCollider.bounds.center, Vector2.down * (bodyCollider.bounds.extents.y + bufferJumpDistance), debugRayColor);*/
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        state();

        // GET WALKING DIRECTION
        walkDirection = Input.GetAxisRaw("Horizontal");

        // FLIPPING SPRITE DIRECTION
        if (walkDirection < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (walkDirection > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (IsGrounded())
        {
            CanJump = true;
        }
        else
        {
            CanJump = false;
        }
        // JUMP BUTTON PRESSING, BUFFERING AND RELEASING
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(jumpKey))
        {
            jumpRelease = false;
            if (IsGrounded() || CanBufferJump(bufferJumpDistance) || GetComponent<DoubleJump>().JumpsLeft > 0)
            {
                jumpPress = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button1) || Input.GetKeyUp(jumpKey))
        {
            jumpRelease = true;
            jumpPress = false;
        }
    }

    void FixedUpdate()
    {
        if (jumpRelease && rb.velocity.y > 0 && !IsGrounded())
        {
            rb.AddForce(new Vector2(0, -jumpStopForce));
        }
        else
        {
            jumpRelease = false;
            if (jumpPress && CanJump)
            {
                Jump();
            }
        }
        rb.velocity = new Vector2(walkDirection * walkSpeed, Mathf.Clamp(rb.velocity.y, -maxYVelocity, maxYVelocity));
    }

    // GROUND CHECK
    public bool IsGrounded()
    {
        RaycastHit2D feetray = Physics2D.BoxCast(bodyCollider.bounds.center, bodyCollider.bounds.size, 0f, Vector2.down, extraColliderHeight, ground);
        return feetray.collider != null;
    }
    public bool CanBufferJump(float closeGroundDistance)
    {
        RaycastHit2D floorcheck = Physics2D.Raycast(bodyCollider.bounds.center - new Vector3(0, bodyCollider.bounds.extents.y, 0), Vector2.down, closeGroundDistance, ground);
        return floorcheck.collider != null;
    }

    public void Jump()
    {
        jumpPress = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce * 100));
    }

    public void state()
    {
        animator.speed = 1;
        if (!IsGrounded())
        {
            animator.SetBool("slowWalk", false);
            animator.SetBool("fastWalk", false);
            if (rb.velocity.y > 0)
            {
                animator.SetBool("launchUp", true);
                animator.SetBool("falling", false);
            }
            else
            {
                animator.SetBool("falling", true);
                animator.SetBool("launchUp", false);
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > walkSpeed / 2)
        {
            animator.SetBool("fastWalk", true);
            animator.SetBool("slowWalk", false);
            animator.SetBool("falling", false);
            animator.SetBool("launchUp", false);
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.01f)
        {
            animator.speed = Mathf.Max(.7f, Mathf.Abs(rb.velocity.x) / (walkSpeed / 2));
            animator.SetBool("slowWalk", true);
            animator.SetBool("fastWalk", false);
            animator.SetBool("falling", false);
            animator.SetBool("launchUp", false);
        }
        else
        {
            animator.SetBool("slowWalk", false);
            animator.SetBool("fastWalk", false);
            animator.SetBool("falling", false);
            animator.SetBool("launchUp", false);
        }
    }
}
