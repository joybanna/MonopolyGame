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
        back_btn.onClick.AddListener(() => Onclick_back());
        play_btn.onClick.AddListener(() => Onclick_play());
    }
    private void LateUpdate()
    {
        if (PhotonNetwork.InRoom)
        {
            roomname_text.text = PhotonNetwork.CurrentRoom.Name;

            if (currentMember != PhotonNetwork.PlayerList.Length)
            {
                Debug.Log("PhotonNetwork.PlayerList.Length : " + PhotonNetwork.PlayerList.Length);
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
        foreach (Player playerInRoom in PhotonNetwork.PlayerList)
        {
            createMember.CreatTag(playerInRoom);
        }
    }
    private void Onclick_back()
    {
        PhotonNetwork.LeaveRoom();
        pageController.SetCanvas(pageController.lobby_canvas);
    }
    private void Onclick_play()
    {

        Debug.Log("play");
        PhotonNetwork.LoadLevel(1);
    }

}
