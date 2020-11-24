using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MasterClientSettingController : MonoBehaviourPunCallbacks
{
    public SpawnController spawnController;
    public RPCController rPCController;
    private void Start()
    {
        SettingBegin();
        rPCController.SpawnStone();
    }
    void SettingBegin()
    {
        spawnController.stones_players = new List<Stone>();
        rPCController.masterClientSettingController = this;
    }
}
