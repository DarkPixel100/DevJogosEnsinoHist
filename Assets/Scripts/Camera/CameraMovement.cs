using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Variável geral da velocidade de movimentação da câmera
    public int moveSpeed;
    [Range(0, 20)]
    // Determinando se o jogador deve ser seguido e a velocidade com que isso é feito
    public bool followPlayer;
    public int followSpeed;
    [Range(0, 20)]
    // Parâmetros para movimentação vertical
    private Vector3 moveOffset;
    public int lookMoveSpeed;
    public float moveDistance;
    public float moveWait;

    // Determinando o objeto "Jogador"
    private GameObject player;

    // Altura da câmera em relação ao jogador
    public float baseCamHeight;

    // Direção em que o jogador está olhando
    private Vector2 lookDir;


    // Variáveis para determinar o tempo que o jogador segura para cima ou para baixo para mover a câmera verticalmente
    private float holdDownStart;
    private float holdDownDuration;

    // Declarando os limites que a câmera pode mostrar
    public float minCamHeight;
    public float levelLength;

    // Declarando parâmetros para responsividade da câmera em telas de diferentes tamanhos

    private float camHalfW;
    private float camConstant = 11f;
    private float camResXOffset;

    void Start()
    {
        // Determinando valores iniciais
        camHalfW = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
        camResXOffset = camHalfW - camConstant;
        player = GameObject.Find("Player");
        followPlayer = true; // Mudável, dependendo da estética
        moveSpeed = followSpeed;
        moveOffset = new Vector3(0f, 0f, 0f);
        transform.position = new Vector3(player.transform.position.x + camResXOffset, player.transform.position.y + baseCamHeight, transform.position.z) + moveOffset;
    }

    void Update()
    {
        // Atualizando parâmetros de responsividade constantemente para caso a resolução seja mudada
        camHalfW = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
        camResXOffset = camHalfW - camConstant;
        // Buscando valores de outros scripts
        lookDir = player.GetComponent<LookAt>().lookDir;
        holdDownStart = player.GetComponent<LookAt>().holdDownStart;
        holdDownDuration = player.GetComponent<LookAt>().holdDownDuration;
        // Fazendo a movimentação vertical após moveWait segundos segurando para cima ou para baixo
        if (holdDownDuration >= moveWait)
        {
            moveSpeed = lookMoveSpeed;
            moveOffset.y = Mathf.RoundToInt(lookDir.y) * moveDistance;
        }
        else
        {
            moveOffset.y = 0;
            moveSpeed = followSpeed;
        }
    }

    void FixedUpdate()
    {
        // Câmera sempre seguindo, no FixedUpdate para seguir cálculos físicos corretamente
        if (followPlayer)
        {
            CamFollow();
        }
    }

    // Movimentação suavizada de seguir o jogador, se mantendo dentro dos limites determinados e seguindo a velocidade definida
    public void CamFollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Min(Mathf.Max(0 + camResXOffset, player.transform.position.x), levelLength - camResXOffset), Mathf.Max(player.transform.position.y + baseCamHeight, minCamHeight), transform.position.z) + moveOffset, moveSpeed * Time.deltaTime);
    }
}
