using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TurnManagement : MonoBehaviourPunCallbacks
{
    [HideInInspector] public GameControllerCenter gameControllerCenter;
    private int num_player;
    private Player now_playerTurn;
    public List<Player> playersOrdered;
    private RPCController rPCController;
    private int maxTurn;
    private int playerWin;
    public void SettingStart(List<Player> _playersOrdered)
    {
        rPCController = gameControllerCenter.rPCController;
        playersOrdered = new List<Player>(_playersOrdered);
        num_player = 0;
        playerWin = 0;
        maxTurn = playersOrdered.Count;
    }
    public void TurnRun()
    {
        playerWin = WinCount();

        if (playerWin < maxTurn)//add -1 on back maxturn for win 3/4
        {
            if (num_player < maxTurn)
            {
                //Debug.Log(num_player + " +rPCController.SendTurn : " + playersOrdered[num_player].NickName);
                if (!IsWin(playersOrdered[num_player].NickName))
                {
                    rPCController.SendTurn(playersOrdered[num_player].NickName);
                    now_playerTurn = playersOrdered[num_player];
                    num_player++;
                }
                else
                {
                    num_player++;
                    TurnRun();
                }
            }
            else
            {
                num_player = 0;
                if (!IsWin(playersOrdered[num_player].NickName))
                {
                    //Debug.Log(num_player + " +rPCController.SendTurn : " + playersOrdered[num_player].NickName);
                    rPCController.SendTurn(playersOrdered[num_player].NickName);
                    now_playerTurn = playersOrdered[num_player];
                    num_player++;
                }
                else
                {
                    num_player++;
                    TurnRun();
                }

            }
        }
        else
        {
            Debug.Log("ENG Game");
        }



    }
    private bool IsWin(string _name)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == _name)
            {
                if ((bool)player.CustomProperties["isWin"])
                {
                    return true;
                }
            }
        }
        return false;
    }
    private int WinCount()
    {
        int temp_playerWin = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if ((bool)player.CustomProperties["isWin"])
            {
                temp_playerWin++;
            }
        }
        return temp_playerWin;
    }

    public void SetPlayerWin()
    {
        Debug.Log("SetPlayerWin");
        PhotonNetwork.LocalPlayer.CustomProperties["isWin"] = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    }
}
