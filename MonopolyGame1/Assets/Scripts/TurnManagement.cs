using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TurnManagement : MonoBehaviourPunCallbacks
{
    private int num_player;
    private Player now_playerTurn;
    private List<Player> playersOrdered;
    public RPCController rPCController;
    private int maxTurn;

    public void SettingStart(List<Player> _playersOrdered)
    {
        playersOrdered = _playersOrdered;
        num_player = 0;
        maxTurn = playersOrdered.Count;
    }
    public void TurnRun()
    {

        if (num_player < maxTurn)
        {
            Debug.Log(num_player + " +rPCController.SendTurn : " + playersOrdered[num_player].NickName);
            rPCController.SendTurn(playersOrdered[num_player].NickName);
            num_player++;
        }
        else
        {
            num_player = 0;
            Debug.Log(num_player + " +rPCController.SendTurn : " + playersOrdered[num_player].NickName);
            rPCController.SendTurn(playersOrdered[num_player].NickName);
        }


    }
}
