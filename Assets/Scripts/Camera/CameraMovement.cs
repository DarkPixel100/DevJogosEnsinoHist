using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool followPlayer;
    public int moveSpeed;
    [Range(0, 20)]
    public int followSpeed;
    [Range(0, 20)]
    public int lookMoveSpeed;
    private GameObject player;

    private Vector3 moveOffset;
    public float baseCamHeight;

    public float moveDistance;
    public float moveWait;

    private Vector2 lookDir;
    private float holdDownStart;
    private float holdDownDuration;

    public float minCamHeight;
    public float levelLength;

    private float camHalfW;
    private float camConstant = 11f;
    private float camResXOffset;

    void Start()
    {
        camHalfW = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
        camResXOffset = camHalfW - camConstant;
        player = GameObject.Find("Player");
        followPlayer = true; // Mudável, dependendo da estética
        moveSpeed = followSpeed;
        moveOffset = new Vector3(0f, 0f, 0f);
        transform.position = new Vector3(player.transform.position.x + camResXOffset, player.transform.position.y + baseCamHeight, -10f) + moveOffset;
    }

    void Update()
    {
        camHalfW = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
        camResXOffset = camHalfW - camConstant;
        lookDir = player.GetComponent<LookAt>().lookDir;
        holdDownStart = player.GetComponent<LookAt>().holdDownStart;
        holdDownDuration = player.GetComponent<LookAt>().holdDownDuration;
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
        if (followPlayer)
        {
            CamFollow();
        }
    }

    public void CamFollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Min(Mathf.Max(0 + camResXOffset, player.transform.position.x), levelLength - camResXOffset), Mathf.Max(player.transform.position.y + baseCamHeight, minCamHeight), -10f) + moveOffset, moveSpeed * Time.deltaTime);
    }
}
