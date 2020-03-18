using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    
    private void Start()
    {

    }
    
    public override void OnConnectedToMaster()
    {
        
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
    
    }
    
    public void Connect()
    {
        
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    
    }
    
    public override void OnJoinedRoom()
    {
    
    }
}