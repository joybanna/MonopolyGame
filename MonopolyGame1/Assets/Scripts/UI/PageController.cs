using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PageController : MonoBehaviourPunCallbacks
{
    public GameObject home_canvas;
    public GameObject lobby_canvas;
    public GameObject room_canvas;
    public Button play_btn;
    public Text status_text;
    private List<GameObject> listCanvas;

    private void Start()
    {
        listCanvas = new List<GameObject>() { home_canvas, lobby_canvas, room_canvas };
        SetCanvas(home_canvas);
        play_btn.onClick.AddListener(() => Onclick_play());
    }
    public void SetCanvas(GameObject _activeCanvas)
    {
        foreach (GameObject Objcanvas in listCanvas)
        {
            if (Objcanvas == _activeCanvas)
            {
                _activeCanvas.SetActive(true);
            }
            else
            {
                Objcanvas.SetActive(false);
            }
        }
    }
    private void Onclick_play()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        SetCanvas(lobby_canvas);
    }
    private void Update()
    {
        status_text.text = "status : " + PhotonNetwork.NetworkClientState.ToString();
    }
}
