using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Vector2 lookDir;
    public float holdDownStart;
    public float holdDownDuration;

    void Start()
    {
        holdDownStart = 0f;
        holdDownDuration = 0f;
        lookDir.x = 1;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            lookDir.x = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        }
        lookDir.y = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        if (lookDir.y == 0 || !this.GetComponent<CharBaseMov>().IsGrounded())
        {
            holdDownDuration = 0f;
            holdDownStart = Time.time;
        }
        else
        {
            holdDownDuration = Time.time - holdDownStart;
        }
    }

}
