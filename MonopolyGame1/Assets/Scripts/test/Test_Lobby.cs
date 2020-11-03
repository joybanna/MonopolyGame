using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Test_Lobby : MonoBehaviourPunCallbacks
{
    public Button connect_btn, randomJoin_btn, createroom_btn, joinroom_btn, disconnect_btn, leaveroom_btn;
    public Text status_text;
    public Text room_text;
    public InputField inputtext;

    private void Start()
    {
        connect_btn.onClick.AddListener(() => Onclick_Connect());
        createroom_btn.onClick.AddListener(() => Onclick_createroom());
        joinroom_btn.onClick.AddListener(() => Onclick_joinroom());
        randomJoin_btn.onClick.AddListener(() => Onclick_RandomJoin());
        disconnect_btn.onClick.AddListener(() => Onclick_disconnect());
        leaveroom_btn.onClick.AddListener(() => Onclick_leaveroom());

    }
    private void Onclick_Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Onclick_RandomJoin()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    private void Onclick_createroom()
    {
        RoomOptions room = new RoomOptions();
        room.IsVisible = true;
        room.IsOpen = true;
        room.MaxPlayers = 4;
        Debug.Log("CreateRoom");
        PhotonNetwork.JoinOrCreateRoom(inputtext.text, room, TypedLobby.Default);
    }
    private void Onclick_joinroom()
    {
        PhotonNetwork.JoinRoom(inputtext.text);
    }
    private void Onclick_disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    private void Onclick_leaveroom()
    {
        PhotonNetwork.LeaveRoom();
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    private void Update()
    {
        status_text.text = "Status : " + PhotonNetwork.NetworkClientState.ToString();
        if (PhotonNetwork.InRoom)
        {
            room_text.text = "Room : " + PhotonNetwork.CurrentRoom.Name + " player count :" + PhotonNetwork.CurrentRoom.PlayerCount;
        }
        else
        {
            room_text.text = "Room : none join";
        }
    }
    public override void OnJoinedRoom()
    {
        room_text.text = "Room : " + PhotonNetwork.CurrentRoom.Name + " player count :" + PhotonNetwork.CurrentRoom.PlayerCount;
    }
}
