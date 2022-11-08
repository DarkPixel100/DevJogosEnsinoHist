using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttack : MonoBehaviour
{
    public float throwInterval;
    public float throwVel;
    public GameObject projectilePref;
    private int lookDir;

    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player"); ;
        if (GetComponent<EnemyMovement>().alternatingWalk)
        {
            this.enabled = false;
        }
        else
        {
            StartCoroutine(throwRepeat(throwInterval));

        }
    }
    void Update()
    {
        lookDir = GetComponent<EnemyMovement>().lookDir;
    }

    IEnumerator throwRepeat(float t)
    {
        while (true)
        {
            yield return new WaitForSeconds(t);
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 15) shoot();
        }
    }

    private void shoot()
    {
        Vector3 instPos = new Vector3(transform.position.x + transform.localScale.x * lookDir, transform.position.y + transform.localScale.y / 2, 0);
        Vector3 posDiff = player.transform.position - instPos;
        GameObject projReal = Instantiate(projectilePref, new Vector3(instPos.x, instPos.y, transform.position.z), Quaternion.identity);
        projReal.GetComponent<ProjectileFunctions>().rotationDirection = -lookDir;
        projReal.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
        projReal.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(posDiff) * throwVel;
    }
}
