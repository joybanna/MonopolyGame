using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer
{
    public string PlayerName;
    public int playerType;
    public int playerNo;
    public DataPlayer(string _PlayerName, int _playerType, int _playerNo)
    {
        PlayerName = _PlayerName;
        playerType = _playerType;
        playerNo = _playerNo;
    }

}
