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
    public CharacterSelect characterSelect;

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
    public void Onclick_play()
    {
        characterSelect.OnEnableScript(false);
        FindObjectOfType<UISoundBox>().PalySoundEffect("click");
        PhotonNetwork.ConnectUsingSettings();
        SetCanvas(lobby_canvas);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

    }
    private void Update()
    {
        status_text.text = "status : " + PhotonNetwork.NetworkClientState.ToString();
    }
}
