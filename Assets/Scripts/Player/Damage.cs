using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private LayerMask damagingLayer;
    private GameObject healthMeter;
    private bool canBeHit;
    public float hittableDelay;
    public float movementDelay;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthMeter = GameObject.Find("HealthMeter");
        healthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health);
        canBeHit = true;
    }

    private void OnTriggerEnter2D(Collider2D damagingElement)
    {
        if (((1 << damagingElement.gameObject.layer) & damagingLayer) != 0 && canBeHit)
        {
            if (damagingElement.name == "BottomlessTrigger")
            {
                GetComponent<HealthNDeath>().health = 0;
                healthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health);
            }
            else if (rb.velocity.y >= -1e-04 || !damagingElement.name.Contains("Enemy"))
            {
                GetComponent<HealthNDeath>().health -= 1;
                canBeHit = false;
                StartCoroutine(hitAllowTimer(hittableDelay));
                healthMeter.GetComponent<HealthMeterKeeper>().UpdateHealth(GetComponent<HealthNDeath>().health);
                rb.simulated = false;
                StartCoroutine(stoppedTimer(movementDelay));
            }
            else
            {
                GameObject.Destroy(damagingElement.gameObject);
            }
        }
    }
    IEnumerator hitAllowTimer(float t)
    {
        yield return new WaitForSeconds(t);
        canBeHit = true;
    }

    IEnumerator stoppedTimer(float t)
    {
        yield return new WaitForSeconds(t);
        rb.simulated = true;
    }
}
