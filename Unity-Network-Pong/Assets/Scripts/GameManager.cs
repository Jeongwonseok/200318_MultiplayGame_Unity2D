using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// MonoBehaviourPunCallbacks : Photon Pun의 이벤트 감지 가능
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private static GameManager instance;

    public Text scoreText;
    public Transform[] spawnPositions; // 플레이어 위치
    public GameObject playerPrefab;
    public GameObject ballPrefab;

    private int[] playerScores; // 플레이어 점수 저장

    private void Start()
    {
        playerScores = new[] { 0, 0 };
        SpawnPlayer();

        // 방장만 공 생성 가능
        if(PhotonNetwork.IsMasterClient)
        {
            SpawnBall();
        }
    }

    private void SpawnPlayer()
    {
        // 플레이어 인덱스 가져오기 (-1 해준다.)
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length]; // 나머지 값 이용해서 예외 처리

        // PhotonNetwork.Instantiate() >> 복제 생성하는 방법 : 로컬에서 생성하고 리모트에 복제한다. -> 복제 생성할 이름을 매개변수로 받는다. & Resources 폴더에 있어야한다.
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }

    private void SpawnBall()
    {
        PhotonNetwork.Instantiate(ballPrefab.name, Vector2.zero, Quaternion.identity);
    }

    // 접속했던 플레이어가 나갈때 실행 >> 나 자신이 떠날때만 실행 >> pun에의해 자동 실행
    // 만약 방 나가기 기능 구현하고 싶을때 >> 나가기 버튼 추가 및 PhotonNetwork.LeaveRoom() 코드 추가
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    // 점수 계산 >> 마스터에서만 계산할 수 있도록!!
    public void AddScore(int playerNumber, int score)
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        playerScores[playerNumber - 1] += score;

        // 모든 클라이언트에게 바뀐 점수를 동기화 해준다. 
        photonView.RPC("RPCUpdateScoreText", RpcTarget.All, playerScores[0].ToString(), playerScores[1].ToString());
    }

    
    [PunRPC]
    private void RPCUpdateScoreText(string player1ScoreText, string player2ScoreText)
    {
        scoreText.text = $"{player1ScoreText} : {player2ScoreText}";
    }
}