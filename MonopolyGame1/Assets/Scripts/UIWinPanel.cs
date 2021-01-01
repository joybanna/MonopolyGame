using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class UIWinPanel : MonoBehaviourPunCallbacks
{
    public Text playerwin_text;
    public Button home_btn;
    public GameObject winpanel;

    public void OpenWinPanel(string _name)
    {
        winpanel.SetActive(true);
        playerwin_text.text = _name;
        home_btn.onClick.AddListener(() => Onclick_home());
    }
    public void Onclick_home()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
