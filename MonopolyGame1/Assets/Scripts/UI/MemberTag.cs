using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class MemberTag : MonoBehaviourPunCallbacks
{
    public string nameplayer_str;
    public Text nameplayer;
    public Player player_network;

    public void SetMemberTag()
    {
        nameplayer_str = player_network.NickName;
        nameplayer.text = player_network.NickName + " : " + (TypeCharacter)player_network.CustomProperties["TypeCharacter"] + " character";
    }
}
