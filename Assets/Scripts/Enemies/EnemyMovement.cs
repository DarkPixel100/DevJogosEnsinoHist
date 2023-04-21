using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bodyCollider;
    [System.NonSerialized]
    public int lookDir = 1;
    private GameObject player;
    public bool alternatingWalk; // define se o inimigo deve ficar parado atirando, ou andando
    [Range(0, 10)]
    public int eMoveSpeed;
    [SerializeField] private LayerMask ground;
    private float extraSideColliderDistance = .02f;
    private float extraFloorColliderHeight = .02f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        /* Coisas de debug se colisão

        Color debugRayColor;
        if (!ShouldTurn())
        {
            debugRayColor = Color.green;
        }
        else
        {
            debugRayColor = Color.red;
        }
        Debug.DrawRay(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y - bodyCollider.bounds.extents.y / 2), new Vector2(lookDir, -1), debugRayColor);
        Debug.DrawRay(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y + bodyCollider.bounds.extents.y / 2), new Vector2(lookDir * (bodyCollider.bounds.extents.x + extraSideColliderDistance), 0), debugRayColor);
        Debug.DrawRay(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y - bodyCollider.bounds.extents.y / 2), new Vector2(lookDir * (bodyCollider.bounds.extents.x + extraSideColliderDistance), 0), debugRayColor);
        Debug.DrawRay(bodyCollider.bounds.center, new Vector2(lookDir * (bodyCollider.bounds.extents.x + extraSideColliderDistance), 0), debugRayColor);
        Debug.DrawRay(bodyCollider.bounds.center, new Vector2(0, - (bodyCollider.bounds.extents.y + extraFloorColliderHeight)), debugRayColor);
        */
        if (lookDir > 0) // Define a direção que o sprite está virado
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (alternatingWalk) // Configura o inimigo para que fique andando constantemente
        {
            Destroy(GetComponent<ThrowAttack>());
            GetComponent<Animator>().SetBool("ShouldWalk", true);
            if (ShouldTurn())
            {
                lookDir *= -1;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("ShouldWalk", false);
            lookDir = Mathf.RoundToInt((player.transform.position.x - transform.position.x) / Mathf.Abs(player.transform.position.x - transform.position.x));
        }
    }
    void FixedUpdate()
    {
        if (alternatingWalk) // Movimentação do inimigo
        {
            rb.velocity = new Vector3(eMoveSpeed * lookDir, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public bool ShouldTurn() // Colisões que definem se o inimigo deve virar para o outro lado ao caminhar
    {
        // Detecta bordas de plataformas
        RaycastHit2D ledgeray = Physics2D.Raycast(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y - bodyCollider.bounds.extents.y / 2), new Vector2(lookDir, -1), 1, ground);

        // Detecta colisão horiontal na altura da cabeça
        RaycastHit2D siderayTop = Physics2D.Raycast(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y + bodyCollider.bounds.extents.y / 4), new Vector2(lookDir, 0), bodyCollider.bounds.extents.x + extraSideColliderDistance, ground);

        // Detecta colisão horiontal na altura dos pés
        RaycastHit2D siderayBottom = Physics2D.Raycast(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y - bodyCollider.bounds.extents.y / 4), new Vector2(lookDir, 0), bodyCollider.bounds.extents.x + extraSideColliderDistance, ground);

        // Detecta se o inimigo está no chão
        RaycastHit2D feetray = Physics2D.Raycast(bodyCollider.bounds.center, Vector2.down, bodyCollider.bounds.extents.y + extraFloorColliderHeight, ground);

        return (ledgeray.collider == null && feetray.collider != null) || (siderayTop.collider != null || siderayBottom.collider != null);
    }
}
