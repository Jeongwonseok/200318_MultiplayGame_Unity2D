using Photon.Pun;
using UnityEngine;

// MonoBehaviourPun : 자기자신의 게임오브젝트에 추가되어있는 photon view 스크립트로 즉시 접근할수 있게 해줌 
public class Ball : MonoBehaviourPun
{
    // IsMasterClientLocal : 현재 스크립트를 실행하고 있는 컴퓨터가 방장(IsMasterClient)인지 물어본다. 동시에 게임 오브젝트가 현재 컴퓨터 내에서 생성된 로컬 게임오브젝트인지 물어본다.
    // 공은 먼저 방장에서 생성 >> 이후 리모트에게 동기화한다.
    // 공의 액션은 방장의 방에서만 작동한다.
    public bool IsMasterClientLocal => PhotonNetwork.IsMasterClient && photonView.IsMine;

    private Vector2 direction = Vector2.right; // 공이 움직이는 방향
    private readonly float speed = 10f; // 속도
    private readonly float randomRefectionIntensity = 0.1f; // 입사각 반사각이 조금씩 랜덤하게 달라지게 >> 게임적으로 재밌게 구현

    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        // 현재 스크립트를 실행하는 측이 호스트가 아니면 리턴
        if(!IsMasterClientLocal || PhotonNetwork.PlayerList.Length < 2)
        {
            return;
        }

        var distance = speed * Time.deltaTime; // 시간만큼 이동할 거리
        // direction 방향으로 distance만큼 이동했을때, 공의 정보를 받아옴
        var hit = Physics2D.Raycast(transform.position, direction, distance);

        // 해당 방향으로 거리만큼 이동했는데 콜라이더에 충돌했다면
        if(hit.collider != null)
        {
            audio.Play();

            var goalPost = hit.collider.GetComponent<Goalpost>();

            // 골 포스트에 도달했는지 확인 & 누가 득점하는지 조건
            if (goalPost != null)
            {
                if(goalPost.playerNumber == 1)
                {
                    GameManager.Instance.AddScore(2 , 1);
                }
                else if(goalPost.playerNumber == 2)
                {
                    GameManager.Instance.AddScore(1 , 1);
                }
            }

            // 입사각에 대응하는 반사각 방향 생성
            direction = Vector2.Reflect(direction, hit.normal);
            // 일부러 오차 주기 >> 재밌게!! (Random.insideUnitCircle : 랜덤한 vector2 를 생성해주는 프로퍼티)
            direction += Random.insideUnitCircle * randomRefectionIntensity;
        }

        transform.position = (Vector2)transform.position + direction * distance;
    }
}