using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RPCController : MonoBehaviourPunCallbacks
{
    [HideInInspector] public GameControllerCenter gameControllerCenter;
    public PhotonView photonView_player, photonView_client;
    [HideInInspector] public Stone myStone;
    [HideInInspector] public Route route;
    private SpawnController spawnController;
    public void SpawnStone()
    {
        spawnController = GetComponent<SpawnController>();
        spawnController.SettingSpawnController(this);
        spawnController.Spawn();
    }

    public void RegisterNode(string _code)
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_RegisterNode", RpcTarget.All, _code);
    }

    public void UnregisterNode(string _code)
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_UnregisterNode", RpcTarget.All, _code);
    }

    public void CheckOrderPlayer()
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_CheckOrderPlayer", RpcTarget.All);
    }

    public void SendSpawnPlayer(string _name)
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_SendSpawnPlayer", RpcTarget.All, _name);
    }
    public void SetPositionStart(string _name, int _index)
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_SetPositionStart", RpcTarget.All, _name, _index);
    }
    public void CreateOrderplayerTagUI(string _name, string _order)
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_CreateOrderplayerTagUI", RpcTarget.All, _name, _order);
    }
    public void SendReadyToMaster()
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_SendReadyToMaster", RpcTarget.MasterClient);
    }
    public void SendTurn(string _name)
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_SendTurn", RpcTarget.All, _name);
    }
    public void SendTurnRunToMaster()
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_SendTurnRunToMaster", RpcTarget.MasterClient);
    }
    public void SettingNodeProp(string _code)
    {
        if (PhotonNetwork.IsConnected) photonView_client.RPC("RPC_SettingNodeProp", RpcTarget.All, _code);
    }

    #region PunRPC

    [PunRPC]
    private void RPC_SentReadyToMaster()
    {
        Debug.Log("master recieve data all ready");
    }
    [PunRPC]
    private void RPC_RegisterNode(string _code)//index|sub
    {
        string[] code = StringSplit(_code);
        //Debug.Log("code-node-subnode : Register :" + _numcode + "-" + temp_numNode + "-" + temp_subNode);
        route.nodeMemberList[int.Parse(code[0])].isStays[int.Parse(code[1])] = true;
    }
    [PunRPC]
    private void RPC_UnregisterNode(string _code)//index|sub
    {
        string[] code = StringSplit(_code);
        //Debug.Log("node-subnode : Unregister :" + _numcode + "-" + temp_numNode + "-" + temp_subNode);
        route.nodeMemberList[int.Parse(code[0])].isStays[int.Parse(code[1])] = false;
    }
    [PunRPC]
    private void RPC_CheckOrderPlayer()
    {
        Debug.Log("my order value : " + PhotonNetwork.LocalPlayer.CustomProperties["playerOrder"]);
    }
    [PunRPC]
    private void RPC_SendSpawnPlayer(string _name)
    {
        Debug.Log("spawn player name : " + PhotonNetwork.LocalPlayer.CustomProperties["playerName"]);
        if (_name == PhotonNetwork.LocalPlayer.NickName)
        {
            SpawnStone();
        }
    }
    [PunRPC]
    private void RPC_SetPositionStart(string _name, int _index)
    {
        if (_name == PhotonNetwork.LocalPlayer.NickName)
        {
            spawnController.SetPosition(_index);
        }
    }
    [PunRPC]
    private void RPC_CreateOrderplayerTagUI(string _name, string _order)
    {
        gameControllerCenter.uIGameController.FillterCreateOrderPlayer(_name, _order);
    }
    [PunRPC]
    private void RPC_SendReadyToMaster()
    {
        //Debug.Log("master recieve RPC_SendReadyToMaster");
        StartCoroutine(gameControllerCenter.masterClientSettingController.MasterWork());

    }
    [PunRPC]
    private void RPC_SendTurn(string _name)
    {
        gameControllerCenter.uIGameController.MyTurn(_name);

        if (_name == PhotonNetwork.LocalPlayer.NickName)
        {
            gameControllerCenter.uIGameController.OpenRollController(true);
        }
        else
        {
            gameControllerCenter.uIGameController.OpenRollController(false);
        }
    }
    [PunRPC]
    private void RPC_SendTurnRunToMaster()
    {
        //Debug.Log("master recieve RPC_SendTurnRunToMaster");
        gameControllerCenter.masterClientSettingController.MasterRecieveTurnRun();
    }
    [PunRPC]
    private void RPC_SettingNodeProp(string _code)
    {
        string[] code = StringSplit(_code);
        gameControllerCenter.generateNodeProperty.SettingNodeProp(int.Parse(code[0]), int.Parse(code[1]), code[2]);
    }
    #endregion

    private string[] StringSplit(string _string)
    {
        string[] split = { ",", ".", "|" };
        string[] word = _string.Split(split, System.StringSplitOptions.RemoveEmptyEntries);
        return word;
    }
}
