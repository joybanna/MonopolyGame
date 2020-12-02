using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIGameController : MonoBehaviourPunCallbacks
{
    public Transform positionPlayerOrder;
    public Transform positionRollNumber;
    public PlayerOrderTag playerOrderTag_prefab;
    public List<PlayerOrderTag> playerOrderTagList;

    public void SettingStart()
    {
        positionRollNumber.gameObject.SetActive(false);
        playerOrderTagList = new List<PlayerOrderTag>();

    }
    public void FillterCreateOrderPlayer(string _name, string _order)
    {
        CreateCardOrderPlayer(_name, _order, PhotonNetwork.LocalPlayer.NickName == _name);
    }
    private void CreateCardOrderPlayer(string _name, string _order, bool _isMine)
    {
        PlayerOrderTag temp = Instantiate(playerOrderTag_prefab, positionPlayerOrder);
        temp.SettingStart(_name, _order);
        temp.SettingStatus("null");
        temp.SettingColor(_isMine);
        playerOrderTagList.Add(temp);
    }
    public void MyTurn(string _name)
    {
        foreach (PlayerOrderTag item in playerOrderTagList)
        {
            if (_name == item.name_text.text)
            {
                item.SettingStatus("myturn");
            }
            else
            {
                item.SettingStatus("null");
            }
        }
    }


}
