using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomController : MonoBehaviourPunCallbacks
{
    public Text roomname_text;
    public CreateMemberTag createMember;
    private int currentMember;
    public Button back_btn;
    public Button play_btn;
    public PageController pageController;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        back_btn.onClick.AddListener(() => Onclick_back());
        play_btn.onClick.AddListener(() => Onclick_play());
    }
    private void LateUpdate()
    {
        if (PhotonNetwork.InRoom)
        {
            if (currentMember != PhotonNetwork.PlayerList.Length)
            {
                currentMember = PhotonNetwork.PlayerList.Length;
                createMember.ClearTag();
                CheckMember();
            }


            if (PhotonNetwork.IsMasterClient)
            {
                play_btn.gameObject.SetActive(true);
            }
            else
            {
                play_btn.gameObject.SetActive(false);
            }
        }
    }
    private void CheckMember()
    {
        //Debug.Log("CheckMember " + currentMember + " ? " + PhotonNetwork.PlayerList.Length);

        foreach (Player playerInRoom in PhotonNetwork.PlayerList)
        {
            createMember.CreatTag(playerInRoom);
        }

    }
    private void Onclick_back()
    {
        FindObjectOfType<UISoundBox>().PalySoundEffect("click");
        PhotonNetwork.LeaveRoom();
    }
    private void Onclick_play()
    {
        Debug.Log("play");
        FindObjectOfType<UISoundBox>().PalySoundEffect("click");
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnJoinedRoom()
    {
        //this.enabled = true;
        roomname_text.text = PhotonNetwork.CurrentRoom.Name;
        //Debug.Log("romm member PC|CP : " + PhotonNetwork.CurrentRoom.PlayerCount + " | " + createMember.playerslistTag.Count);
        //CheckMember();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
        currentMember--;
        createMember.RemoveTag(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable());
        pageController.SetCanvas(pageController.lobby_canvas);
        PhotonNetwork.JoinLobby();
        base.OnLeftRoom();
    }

}
