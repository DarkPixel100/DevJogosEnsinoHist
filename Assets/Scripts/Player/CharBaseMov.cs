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
    public GameObject inputListener;
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

        // Direção de movimento
        walkDirection = Input.GetAxisRaw("Horizontal");

        // Direção do sprite
        if (walkDirection < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (walkDirection > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (IsGrounded()) // Se estiver no chão, pode pular
        {
            CanJump = true;
        }
        else
        {
            CanJump = false;
        }
        // Se apertar o botão enquanto está no chão (ou com pulos restantes, no caso de mais de 1 pulo por vez), pula
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(jumpKey))
        {
            jumpRelease = false;
            if (IsGrounded() || CanBufferJump(bufferJumpDistance) || GetComponent<DoubleJump>().JumpsLeft > 0)
            {
                jumpPress = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button1) || Input.GetKeyUp(jumpKey)) // Soltou o botão
        {
            jumpRelease = true;
            jumpPress = false;
        }
    }

    void FixedUpdate()
    {
        if (jumpRelease && rb.velocity.y > 0 && !IsGrounded()) // Se soltou o botão enquanto sobe, é aplicada uma força contrária para parar o movimento mais cedo
        {
            rb.AddForce(new Vector2(0, -jumpStopForce)); // Força contrária
        }
        else
        {
            jumpRelease = false;
            if (jumpPress && CanJump) // Pode pular antes de necessariamente chegar ao chão
            {
                Jump();
            }
        }
        rb.velocity = new Vector2(walkDirection * walkSpeed, Mathf.Clamp(rb.velocity.y, -maxYVelocity, maxYVelocity)); // Denota a velocidade horizontal e limita e velocidade vertical
    }

    // Checa o contato com o chão
    public bool IsGrounded()
    {
        // Colisão com o chão a partir dos pés
        RaycastHit2D feetray = Physics2D.BoxCast(bodyCollider.bounds.center, bodyCollider.bounds.size, 0f, Vector2.down, extraColliderHeight, ground);
        return feetray.collider != null;
    }
    public bool CanBufferJump(float closeGroundDistance)
    {
        // Distância específica do chão que permite fazer um buffer do potão de pular, para que pule automaticamente quando encostar no chão
        RaycastHit2D floorcheck = Physics2D.Raycast(bodyCollider.bounds.center - new Vector3(0, bodyCollider.bounds.extents.y, 0), Vector2.down, closeGroundDistance, ground);
        return floorcheck.collider != null;
    }

    public void Jump() // Função pular
    {
        GetComponent<PlayerParticles>().CreateDust(); // Cria fumaça ao pular
        GetComponent<AudioPlayer>().PlayAudio("jump"); // Áudio do pulo
        jumpPress = false; // Botão não mais recém precionado
        rb.velocity = new Vector2(rb.velocity.x, 0); // Velocidade vertical zerada
        rb.AddForce(new Vector2(0, jumpForce * 100)); // Adicionando força do pulo
    }

    public void state()
    {
        animator.speed = 1;
        if (!IsGrounded()) // Se não está no chão, animação de pular ou cair
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
        else if (Mathf.Abs(rb.velocity.x) > walkSpeed / 2) // Se está correndo, animação de correr
        {
            animator.SetBool("fastWalk", true);
            animator.SetBool("slowWalk", false);
            animator.SetBool("falling", false);
            animator.SetBool("launchUp", false);
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.01f) // Se está andando devagar, animação de andar
        {
            animator.speed = Mathf.Max(.7f, Mathf.Abs(rb.velocity.x) / (walkSpeed / 2));
            animator.SetBool("slowWalk", true);
            animator.SetBool("fastWalk", false);
            animator.SetBool("falling", false);
            animator.SetBool("launchUp", false);
        }
        else // Senão, animação "idle"
        {
            animator.SetBool("slowWalk", false);
            animator.SetBool("fastWalk", false);
            animator.SetBool("falling", false);
            animator.SetBool("launchUp", false);
        }
    }
}
