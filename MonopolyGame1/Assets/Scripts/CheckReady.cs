using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CheckReady : MonoBehaviourPunCallbacks
{
    public GameControllerCenter gameControllerCenter;
    private RPCController rPCController;

    public void SettingStart()//start
    {
        this.enabled = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        rPCController = gameControllerCenter.rPCController;

        PhotonNetwork.LocalPlayer.CustomProperties["statusPlayer"] = "isLoad";
        PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
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
