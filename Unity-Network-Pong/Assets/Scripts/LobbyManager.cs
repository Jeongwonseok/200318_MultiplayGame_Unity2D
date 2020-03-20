using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

// MonoBehaviourPunCallbacks >> Photon Pun 서비스의 이벤트를 감지할 수 있다.
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // 게임 버전에 따라 멀티플레이 매칭이 가능한지가 정해지기 때문에
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    
    private void Start()
    {
        // 마스터 서버 접속
        PhotonNetwork.GameVersion = gameVersion; // 게임 버전
        PhotonNetwork.ConnectUsingSettings(); // 게임 설정 정보 이용해서 게임 접속 시도

        joinButton.interactable = false;
        connectionInfoText.text = "Connecting To Master Server...";
    }

    // 접속 성공 했을 때
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online : Connected to Master Server";
    }

    // 접속 실패 했을 때
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()} - Try reconnecting...";

        PhotonNetwork.ConnectUsingSettings(); // 다시 접속 시도
    }

    // 조인 버튼 눌렀을 때 클릭리스너
    public void Connect()
    {
        joinButton.interactable = false;

        // 접속이 잘 되어있는 상태라면
        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom(); // 랜덤으로 방에 접속
        }
        else // 접속이 잘 안되어 있다면
        {
            connectionInfoText.text = "Offline : Connection Disabled - Try reconnecting...";

            PhotonNetwork.ConnectUsingSettings(); // 다시 접속 시도
        }
    }

    // 빈방 없을때 발동되는 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "There is no empty room, Creatinf new Room.";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
    
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Connected with Room.";

        // 다른 플레이어 포함해서 씬 전환하기 위해!! >> 동기화까지 해준다.
        PhotonNetwork.LoadLevel("Main");
    }
}