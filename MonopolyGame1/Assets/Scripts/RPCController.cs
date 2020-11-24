using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RPCController : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable playerCustomProperties;
    public PhotonView photonView_player, photonView_client;
    private float time_wait_check = 1f;
    public MasterClientSettingController masterClientSettingController;

    public RandomSteps randomSteps;
    public Route route;

    public void SpawnStone()
    {
        playerCustomProperties = PhotonNetwork.LocalPlayer.CustomProperties;

        GameObject playerStone = PhotonNetwork.Instantiate("stoneplayer_prefab_" + (TypeCharacter)PhotonNetwork.LocalPlayer.CustomProperties["TypeCharacter"], Vector3.zero, Quaternion.Euler(Vector3.zero));
        photonView_player = playerStone.GetComponent<PhotonView>();
        playerStone.GetComponent<Stone>().currentRoute = route;
        playerCustomProperties["isSpawned"] = true;
        playerCustomProperties["photonView_id"] = photonView_player.ViewID;

        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProperties);

        Debug.Log("RPC_SpawnStoned : " + photonView_player.ViewID + " isSpawned : " + (bool)PhotonNetwork.LocalPlayer.CustomProperties["isSpawned"]);

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(WaitForCheckSpawn(time_wait_check));
        }

        randomSteps.stone = playerStone.GetComponent<Stone>();

    }


    [PunRPC]
    public void RPC_CheckAllPlayerSpawned()
    {
        Debug.Log("RPC_CheckAllPlayerSpawned");

        if (IsCheckReady())
        {
            photonView_client.RPC("RPC_SentReadyToMaster", RpcTarget.MasterClient);
        }

    }

    private bool IsCheckReady()
    {
        foreach (Player playerinromm in PhotonNetwork.PlayerList)
        {
            if (!(bool)playerinromm.CustomProperties["isSpawned"])
            {
                Debug.Log("player not ready : " + playerinromm.NickName + " " + (bool)playerinromm.CustomProperties["isSpawned"]);
                return false;
            }
        }
        return true;
    }

    [PunRPC]
    public void RPC_SentReadyToMaster()
    {
        Debug.Log("master recieve data all ready");
    }

    private IEnumerator WaitForCheckSpawn(float _time)
    {
        yield return new WaitForSeconds(_time);
        photonView_client.RPC("RPC_CheckAllPlayerSpawned", RpcTarget.MasterClient);
    }



    public void RegisterNode(int _numcode)
    {
        photonView_client.RPC("RPC_RegisterNode", RpcTarget.All, _numcode);
    }
    [PunRPC]
    public void RPC_RegisterNode(int _numcode)
    {

        int temp_subNode = _numcode % 1000;
        int temp_numNode = (_numcode - temp_subNode) / 1000;
        Debug.Log("code-node-subnode : true :" + _numcode + "," + temp_numNode + "," + temp_subNode);
        route.nodeMemberList[temp_numNode].isStays[temp_subNode] = true;
    }

    public void UnregisterNode(int _numcode)
    {
        photonView_client.RPC("RPC_UnregisterNode", RpcTarget.All, _numcode);
    }
    [PunRPC]
    public void RPC_UnregisterNode(int _numcode)
    {
        int temp_subNode = _numcode % 1000;
        int temp_numNode = (_numcode - temp_subNode) / 1000;

        Debug.Log("node-subnode : false :" + _numcode + "," + temp_numNode + "," + temp_subNode);
        route.nodeMemberList[temp_numNode].isStays[temp_subNode] = false;
    }
}
