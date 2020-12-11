using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OrderPlayerSystem
{
    public int GetOrder()
    {
        int temp = Random.Range(100, 999);
        return temp;
    }
    public bool CheckDuplicateOrderValue(List<int> _orderValue)
    {
        int temp = 0;
        foreach (int num in _orderValue)
        {

            if (temp - num == 0)
            {
                Debug.Log("DuplicateOrderValue");
                temp = num;
                return false;
            }
            else
            {
                temp = num;
                return true;
            }
        }

        Debug.LogError("foreach (int num in _orderValue) not work !!!");
        return true;
    }

    public List<int> RandomOrderAllPlayer()
    {
        List<int> orderValue = new List<int>();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            player.CustomProperties["playerOrder"] = GetOrder();
            player.SetCustomProperties(player.CustomProperties);

            //Debug.Log("player : " + player.CustomProperties["playerName"].ToString() + " is value : " + (int)player.CustomProperties["playerOrder"]);
            orderValue.Add((int)player.CustomProperties["playerOrder"]);
        }

        orderValue.Sort();
        return orderValue;
    }
    public List<int> CheckDuplicateOrder(List<int> _orderValue)
    {
        List<int> _neworderValue = new List<int>();

        if (!CheckDuplicateOrderValue(_orderValue))
        {
            _neworderValue = RandomOrderAllPlayer();
            return _neworderValue;
        }
        else
        {
            return _orderValue;
        }
    }
    public List<Player> GiveOrder(List<int> _orderValue)
    {
        List<Player> temps = new List<Player>();
        foreach (int num in _orderValue)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (num == (int)player.CustomProperties["playerOrder"])
                {
                    temps.Add(player);
                }
            }
        }

        return temps;
    }

}
