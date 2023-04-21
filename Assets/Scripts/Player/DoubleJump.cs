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
        if (GetComponent<CharBaseMov>().IsGrounded()) // Reseta a quantidade de pulos ao cair no chão
        {
            JumpsLeft = NumberOfExtraJumps;
        }
    }
    void FixedUpdate()
    {
        if (!GetComponent<CharBaseMov>().IsGrounded() && GetComponent<CharBaseMov>().jumpPress && JumpsLeft > 0) // Se não estiver no chão, e pressionar pulo, tendo 1 ou mais restantes, pula
        {
            JumpsLeft--; // Reduz a quantidade restante de pulos
            GetComponent<CharBaseMov>().Jump(); // Pula
        }
    }
}
