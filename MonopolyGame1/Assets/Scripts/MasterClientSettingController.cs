using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MasterClientSettingController : MonoBehaviourPunCallbacks
{
    //public SpawnController spawnController;
    [HideInInspector] public GameControllerCenter gameControllerCenter;
    private OrderPlayerSystem orderPlayerSystem;
    private List<Player> playersOrdered;
    private List<int> orderValue;
    private bool isWorked = false;
    private bool isSet = false;

    public IEnumerator MasterWork()//call by all client
    {
        if (PhotonNetwork.IsMasterClient && !isWorked && !isSet)
        {
            gameControllerCenter.generateNodeProperty.StartGenProp();
            yield return new WaitForEndOfFrame();
            isWorked = true;
            SettingBegin();
            orderValue = orderPlayerSystem.RandomOrderAllPlayer();
            orderValue = orderPlayerSystem.CheckDuplicateOrder(orderValue);
            //rPCController.CheckOrderPlayer();//debug
            playersOrdered = orderPlayerSystem.GiveOrder(orderValue);

            CreateUIOrderPlayer();

            gameControllerCenter.turnManagement.SettingStart(playersOrdered);

            WaitSpawned();  //spawnplayer with order
            yield return new WaitForEndOfFrame();
            IESetPositionStart();
            yield return new WaitForEndOfFrame();


            MasterRecieveTurnRun();
            isSet = true;
        }
    }
    private void SettingBegin()
    {
        playersOrdered = new List<Player>();
        orderValue = new List<int>();
        orderPlayerSystem = new OrderPlayerSystem();
    }

    private void WaitSpawned()
    {
        for (int i = 0; i < playersOrdered.Count; i++)
        {
            gameControllerCenter.rPCController.SendSpawnPlayer(playersOrdered[i].CustomProperties["playerName"].ToString());
        }

    }
    private void IESetPositionStart()
    {
        for (int i = 0; i < playersOrdered.Count; i++)
        {
            gameControllerCenter.rPCController.SetPositionStart(playersOrdered[i].CustomProperties["playerName"].ToString(), i);
        }
    }

    private void CreateUIOrderPlayer()
    {
        for (int i = 0; i < playersOrdered.Count; i++)
        {
            gameControllerCenter.rPCController.CreateOrderplayerTagUI(playersOrdered[i].NickName, (i + 1).ToString());
        }
    }
    public void MasterRecieveTurnRun()
    {
        gameControllerCenter.turnManagement.TurnRun();
    }
}
