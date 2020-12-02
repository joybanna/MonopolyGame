using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public enum TypeCharacter
{
    normal, fast, slow
}
public class LobbyController : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    public Button back_btn, createroom_btn, joinroom_btn, randomroom_btn;
    public InputField name_inp, room_inp;
    public Dropdown typeChar_drop;
    public PageController pageController;
    private void Start()
    {
        back_btn.onClick.AddListener(() => Onclick_back());
        createroom_btn.onClick.AddListener(() => Onclick_createroom());
        joinroom_btn.onClick.AddListener(() => Onclick_joinroom());
        randomroom_btn.onClick.AddListener(() => Onclick_randomroom());
    }
    private void Onclick_back()
    {
        PhotonNetwork.Disconnect();
        pageController.SetCanvas(pageController.home_canvas);
    }
    private void Onclick_createroom()
    {
        CreateInfoPlayer();
        PhotonNetwork.CreateRoom(room_inp.text, RoomSetting(), TypedLobby.Default);
        pageController.SetCanvas(pageController.room_canvas);
    }
    private void Onclick_joinroom()
    {
        PhotonNetwork.JoinRoom(room_inp.text);
        CreateInfoPlayer();
        pageController.SetCanvas(pageController.room_canvas);
    }
    private void Onclick_randomroom()
    {
        PhotonNetwork.JoinRandomRoom();
        CreateInfoPlayer();
        pageController.SetCanvas(pageController.room_canvas);
    }
    private void CreateInfoPlayer()
    {
        if (name_inp.text == "")
        {
            name_inp.text = "player #" + Random.Range(1000, 9999);
        }

        PhotonNetwork.LocalPlayer.NickName = name_inp.text;
        playerCustomProperties["TypeCharacter"] = (TypeCharacter)typeChar_drop.value;
        playerCustomProperties["isSpawned"] = false;
        playerCustomProperties["photonView_id"] = 0000;
        playerCustomProperties["playerName"] = PhotonNetwork.LocalPlayer.NickName;
        playerCustomProperties["playerOrder"] = 0;
        playerCustomProperties["isWin"] = false;
        playerCustomProperties["statusPlayer"] = "inRoom";
        PhotonNetwork.LocalPlayer.CustomProperties = playerCustomProperties;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

    }
    private RoomOptions RoomSetting()
    {
        if (room_inp.text == "")
        {
            room_inp.text = "Room#" + Random.Range(1000, 9999);
        }
        RoomOptions room = new RoomOptions();
        room.IsVisible = true;
        room.IsOpen = true;
        room.MaxPlayers = 4;
        Debug.Log("CreateRoom");
        return room;
    }



}
