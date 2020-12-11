using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StateCheckClient : MonoBehaviourPunCallbacks
{
    public bool CheckAllClientState(string _keyCheck, string _textCheck)
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties[_keyCheck].ToString() != _textCheck)
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
