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

    public void SettingStart(List<Player> _playersOrdered)
    {
        rPCController = gameControllerCenter.rPCController;
        playersOrdered = new List<Player>(_playersOrdered);
        num_player = 0;
        maxTurn = playersOrdered.Count;
    }
    public void TurnRun()
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
                TurnRun();
            }

        }


    }
    private bool IsWin(string _name)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == _name)
            {
                if (player.CustomProperties[""].ToString() == "")
                {
                    return true;
                }
            }
        }
        return false;
    }
}
