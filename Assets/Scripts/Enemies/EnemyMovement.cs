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
    public bool alternatingWalk;
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
        /*
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
        if (lookDir > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (alternatingWalk)
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
        if (alternatingWalk)
        {
            rb.velocity = new Vector3(eMoveSpeed * lookDir, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public bool ShouldTurn()
    {
        RaycastHit2D ledgeray = Physics2D.Raycast(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y - bodyCollider.bounds.extents.y / 2), new Vector2(lookDir, -1), 1, ground);
        RaycastHit2D siderayTop = Physics2D.Raycast(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y + bodyCollider.bounds.extents.y / 4), new Vector2(lookDir, 0), bodyCollider.bounds.extents.x + extraSideColliderDistance, ground);
        RaycastHit2D siderayBottom = Physics2D.Raycast(new Vector2(bodyCollider.bounds.center.x, bodyCollider.bounds.center.y - bodyCollider.bounds.extents.y / 4), new Vector2(lookDir, 0), bodyCollider.bounds.extents.x + extraSideColliderDistance, ground);
        RaycastHit2D feetray = Physics2D.Raycast(bodyCollider.bounds.center, Vector2.down, bodyCollider.bounds.extents.y + extraFloorColliderHeight, ground);
        return (ledgeray.collider == null && feetray.collider != null) || (siderayTop.collider != null || siderayBottom.collider != null);
    }
}
