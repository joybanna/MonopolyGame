using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CheckReady : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable playerCustomProperties;
    public MasterClientSettingController masterClientSettingController;
    public RPCController rPCController;

    private void Start()
    {
        playerCustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        PhotonNetwork.AutomaticallySyncScene = true;
        masterClientSettingController = FindObjectOfType<MasterClientSettingController>();
        rPCController = FindObjectOfType<RPCController>();
        masterClientSettingController.SettingStart();
        playerCustomProperties["statusPlayer"] = "isLoad";
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);
    }
    private void Update()
    {
        if (CheckAllLoaded())
        {
            this.enabled = false;
            rPCController.SendReadyToMaster();
        }
    }

    public bool CheckAllLoaded()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties["statusPlayer"].ToString() != "isLoad")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

}
