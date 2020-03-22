using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer spriteRenderer;
    
    public float speed = 6f;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 현재 로컬 플레이어의 게임오브젝트라면
        if(photonView.IsMine)
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }

    private void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        var input = InputButton.VerticalInput;

        var distance = input * speed * Time.deltaTime; // 플레이어 이동할 거리
        var targetPosition = transform.position + Vector3.up * distance; // 위 아래 방향으로 타겟 좌표 지정

        playerRigidbody.MovePosition(targetPosition);
    }
}
