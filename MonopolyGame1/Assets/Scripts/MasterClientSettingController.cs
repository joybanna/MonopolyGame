using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MasterClientSettingController : MonoBehaviourPunCallbacks
{
    //public SpawnController spawnController;
    public RPCController rPCController;
    public UIGameController uIGameController;
    private OrderPlayerSystem orderPlayerSystem;
    private List<Player> playersOrdered;
    private List<int> orderValue;
    private bool isWorked = false;
    private TurnManagement turnManagement;

    public void SettingStart()
    {
        uIGameController = FindObjectOfType<UIGameController>();
        turnManagement = FindObjectOfType<TurnManagement>();
        rPCController.uIGameController = uIGameController;
        turnManagement.rPCController = rPCController;
        uIGameController.SettingStart();
    }
    public void MasterWork()
    {
        if (PhotonNetwork.IsMasterClient && !isWorked)
        {
            isWorked = true;
            SettingBegin();
            orderValue = orderPlayerSystem.RandomOrderAllPlayer();
            orderValue = orderPlayerSystem.CheckDuplicateOrder(orderValue);
            //rPCController.CheckOrderPlayer();//debug
            playersOrdered = orderPlayerSystem.GiveOrder(orderValue);

            CreateUIOrderPlayer();

            StartCoroutine(WaitSpawned());  //spawnplayer with order
            StartCoroutine(IESetPositionStart());

            turnManagement.SettingStart(playersOrdered);

        }
    }
    private void SettingBegin()
    {
        playersOrdered = new List<Player>();
        orderValue = new List<int>();
        orderPlayerSystem = new OrderPlayerSystem();
    }
    IEnumerator WaitSpawned()
    {
        foreach (Player player in playersOrdered)
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log("1-WaitSpawned-affter");
            rPCController.SendSpawnPlayer(player.CustomProperties["playerName"].ToString());
            Debug.Log("end-WaitSpawned-continue foreach");
        }
    }

    IEnumerator IESetPositionStart()
    {
        foreach (Player player in playersOrdered)
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log("2-IESetPositionStart-affter");
            rPCController.SetPositionStart(player.CustomProperties["playerName"].ToString());
            Debug.Log("end-IESetPositionStart-continue foreach");
        }
        turnManagement.TurnRun();
    }

    private void CreateUIOrderPlayer()
    {
        for (int i = 0; i < playersOrdered.Count; i++)
        {
            rPCController.CreateOrderplayerTagUI(playersOrdered[i].NickName, (i + 1).ToString());
        }
    }
}
