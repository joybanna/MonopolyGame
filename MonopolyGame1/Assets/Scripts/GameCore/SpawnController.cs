using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnController : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable playerCustomProperties;
    private RPCController rPCController;
    public void SettingSpawnController(RPCController _rPCController)
    {
        rPCController = _rPCController;
        playerCustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        _rPCController.route = _rPCController.gameControllerCenter.route;
    }
    public void Spawn()
    {
        GameObject playerStone = PhotonNetwork.Instantiate("stoneplayer_prefab_" + (TypeCharacter)PhotonNetwork.LocalPlayer.CustomProperties["TypeCharacter"], rPCController.route.childNodeLists[0].position, Quaternion.Euler(Vector3.zero));
        rPCController.photonView_player = playerStone.GetComponent<PhotonView>();
        rPCController.myStone = playerStone.GetComponent<Stone>();
        rPCController.myStone.currentRoute = rPCController.gameControllerCenter.route;
        rPCController.myStone.typeCharacter = (TypeCharacter)PhotonNetwork.LocalPlayer.CustomProperties["TypeCharacter"];
        playerCustomProperties["isSpawned"] = true;
        playerCustomProperties["photonView_id"] = rPCController.photonView_player.ViewID;

        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

        //Debug.Log("RPC_SpawnStoned : " + photonView_player.ViewID + " isSpawned : " + (bool)PhotonNetwork.LocalPlayer.CustomProperties["isSpawned"]);

        rPCController.myStone.GetComponent<SyncTransformStone>().SetSyncTransformStone(rPCController.photonView_player);
        rPCController.gameControllerCenter.randomSteps.stone = rPCController.myStone;
        rPCController.gameControllerCenter.uIGameController.uITestGameCore.SetMaxMinRoll((TypeCharacter)PhotonNetwork.LocalPlayer.CustomProperties["TypeCharacter"]);
    }
    public void SetPosition(int _index)
    {
        Vector3 new_pos = rPCController.route.nodeMemberList[0].all_Positions[_index].position;
        Vector3 new_pos_look = rPCController.route.nodeMemberList[1].transform.position;
        rPCController.myStone.transform.position = new Vector3(new_pos.x, 0, new_pos.z);
        rPCController.myStone.transform.LookAt(new_pos_look);
    }



}
