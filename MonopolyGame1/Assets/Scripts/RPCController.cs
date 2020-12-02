using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RPCController : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable playerCustomProperties;
    public PhotonView photonView_player, photonView_client;
    public Stone myStone;
    public RandomSteps randomSteps;
    public Route route;
    public UIGameController uIGameController;

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

        myStone = playerStone.GetComponent<Stone>();
        myStone.GetComponent<SyncTransformStone>().SetSyncTransformStone(photonView_player);
        randomSteps.stone = myStone;
    }


    public void RegisterNode(int _numcode)
    {
        photonView_client.RPC("RPC_RegisterNode", RpcTarget.All, _numcode);
    }

    public void UnregisterNode(int _numcode)
    {
        photonView_client.RPC("RPC_UnregisterNode", RpcTarget.All, _numcode);
    }

    public void CheckOrderPlayer()
    {
        photonView_client.RPC("RPC_CheckOrderPlayer", RpcTarget.All);
    }

    public void SendSpawnPlayer(string _name)
    {
        photonView_client.RPC("RPC_SendSpawnPlayer", RpcTarget.All, _name);
    }
    public void SetPositionStart(string _name)
    {
        photonView_client.RPC("RPC_SetPositionStart", RpcTarget.All, _name);
    }
    public void CreateOrderplayerTagUI(string _name, string _order)
    {
        photonView_client.RPC("RPC_CreateOrderplayerTagUI", RpcTarget.All, _name, _order);
    }
    public void SendReadyToMaster()
    {
        photonView_client.RPC("RPC_SendReadyToMaster", RpcTarget.MasterClient);
    }
    public void SendTurn(string _name)
    {
        photonView_client.RPC("RPC_SendTurn", RpcTarget.All, _name);

    }

    #region PunRPC

    [PunRPC]
    private void RPC_SentReadyToMaster()
    {
        Debug.Log("master recieve data all ready");
    }
    [PunRPC]
    private void RPC_RegisterNode(int _numcode)
    {
        int temp_subNode = _numcode % 1000;
        int temp_numNode = (_numcode - temp_subNode) / 1000;
        Debug.Log("code-node-subnode : Register :" + _numcode + "-" + temp_numNode + "-" + temp_subNode);
        route.nodeMemberList[temp_numNode].isStays[temp_subNode] = true;
    }
    [PunRPC]
    private void RPC_UnregisterNode(int _numcode)
    {
        int temp_subNode = _numcode % 1000;
        int temp_numNode = (_numcode - temp_subNode) / 1000;

        Debug.Log("node-subnode : Unregister :" + _numcode + "-" + temp_numNode + "-" + temp_subNode);
        route.nodeMemberList[temp_numNode].isStays[temp_subNode] = false;
    }
    [PunRPC]
    private void RPC_CheckOrderPlayer()
    {
        Debug.Log("my order value : " + PhotonNetwork.LocalPlayer.CustomProperties["playerOrder"]);
    }
    [PunRPC]
    private void RPC_SendSpawnPlayer(string _name)
    {
        Debug.Log("string name : " + PhotonNetwork.LocalPlayer.CustomProperties["playerName"]);
        if (_name == PhotonNetwork.LocalPlayer.NickName)
        {
            Debug.Log("name is me ready to spawn");
            SpawnStone();
        }
    }
    [PunRPC]
    private void RPC_SetPositionStart(string _name)
    {
        if (_name == PhotonNetwork.LocalPlayer.NickName)
        {
            myStone.speedMove = 0.001f;
            myStone.MoveSteps(route.childNodeLists.Count);
        }
    }
    [PunRPC]
    private void RPC_CreateOrderplayerTagUI(string _name, string _order)
    {
        uIGameController.FillterCreateOrderPlayer(_name, _order);
    }
    [PunRPC]
    private void RPC_SendReadyToMaster()
    {
        Debug.Log("master recieve RPC_SendReadyToMaster");
        FindObjectOfType<MasterClientSettingController>().MasterWork();
    }
    [PunRPC]
    private void RPC_SendTurn(string _name)
    {
        if (_name == PhotonNetwork.LocalPlayer.NickName)
        {
            uIGameController.MyTurn(_name);
            uIGameController.positionRollNumber.gameObject.SetActive(true);
        }
        else
        {
            uIGameController.positionRollNumber.gameObject.SetActive(false);
        }
    }
    #endregion
}
