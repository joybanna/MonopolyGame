using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
public enum TypeCharacter
{
    normal, fast, slow
}
public class LobbyController : MonoBehaviourPunCallbacks
{
    public Button back_btn, createroom_btn, joinroom_btn, randomroom_btn;
    public InputField name_inp, room_inp;
    public Dropdown typeChar_drop;
    public PageController pageController;
    public DataPlayer dataPlayer;
    public GameObject wait_panel;

    private void Start()
    {

    }
    private void Onclick_back()
    {
        FindObjectOfType<UISoundBox>().PalySoundEffect("click");
        pageController.characterSelect.OnEnableScript(true);
        PhotonNetwork.LeaveLobby();
    }
    private void Onclick_createroom()
    {
        FindObjectOfType<UISoundBox>().PalySoundEffect("click");
        CreateInfoPlayer();
        PhotonNetwork.CreateRoom(room_inp.text, RoomSetting(), TypedLobby.Default);
    }
    private void Onclick_joinroom()
    {
        FindObjectOfType<UISoundBox>().PalySoundEffect("click");
        CreateInfoPlayer();
        PhotonNetwork.JoinRoom(room_inp.text);
    }
    private void Onclick_randomroom()
    {
        FindObjectOfType<UISoundBox>().PalySoundEffect("click");
        CreateInfoPlayer();
        PhotonNetwork.JoinRandomRoom();
    }
    private void CreateInfoPlayer()
    {
        PhotonNetwork.LocalPlayer.CustomProperties = new ExitGames.Client.Photon.Hashtable();

        //Debug.Log("CreateInfoPlayer " + pageController.dataPlayer.playerType);
        PhotonNetwork.LocalPlayer.NickName = name_inp.text;
        PhotonNetwork.LocalPlayer.CustomProperties["TypeCharacter"] = (TypeCharacter)dataPlayer.playerType;
        PhotonNetwork.LocalPlayer.CustomProperties["isSpawned"] = false;
        PhotonNetwork.LocalPlayer.CustomProperties["photonView_id"] = 0000;
        PhotonNetwork.LocalPlayer.CustomProperties["playerName"] = PhotonNetwork.LocalPlayer.NickName;
        PhotonNetwork.LocalPlayer.CustomProperties["playerOrder"] = 0;
        PhotonNetwork.LocalPlayer.CustomProperties["isWin"] = false;
        PhotonNetwork.LocalPlayer.CustomProperties["statusPlayer"] = "inRoom";
        PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    }
    private RoomOptions RoomSetting()
    {
        RoomOptions room = new RoomOptions();
        room.IsVisible = true;
        room.IsOpen = true;
        room.MaxPlayers = 4;
        return room;
    }
    public override void OnJoinedRoom()
    {
        pageController.SetCanvas(pageController.room_canvas);
    }
    public override void OnLeftLobby()
    {
        PhotonNetwork.Disconnect();
        FindObjectOfType<UISoundBox>().PalySoundEffect("disconneted");
        pageController.SetCanvas(pageController.home_canvas);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        FindObjectOfType<UISoundBox>().PalySoundEffect("conneted");
        dataPlayer = new DataPlayer("player #" + Random.Range(1000, 9999), pageController.characterSelect.typeCharacter);
        back_btn.onClick.AddListener(() => Onclick_back());
        createroom_btn.onClick.AddListener(() => Onclick_createroom());
        joinroom_btn.onClick.AddListener(() => Onclick_joinroom());
        randomroom_btn.onClick.AddListener(() => Onclick_randomroom());
        name_inp.text = dataPlayer.PlayerName;
        room_inp.text = "Room#" + Random.Range(1000, 9999);
        wait_panel.SetActive(false);
        base.OnJoinedLobby();
    }
    public override void OnLeftRoom()
    {
        wait_panel.SetActive(true);
        base.OnLeftRoom();
    }
}
