using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public int NumberOfExtraJumps;
    public int JumpsLeft;
    void Start()
    {
        JumpsLeft = NumberOfExtraJumps;
    }

    void Update()
    {
        if (GetComponent<CharBaseMov>().IsGrounded())
        {
            JumpsLeft = NumberOfExtraJumps;
        }
    }
    void FixedUpdate()
    {
        if (!GetComponent<CharBaseMov>().IsGrounded() && GetComponent<CharBaseMov>().jumpPress && JumpsLeft > 0)
        {
            JumpsLeft--;
            GetComponent<CharBaseMov>().Jump();
        }
    }
}
