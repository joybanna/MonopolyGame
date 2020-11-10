using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer
{
    public string PlayerName;
    public TypeCharacter playerType;
    public int playerNo;
    public DataPlayer(string _PlayerName, TypeCharacter _playerType, int _playerNo)
    {
        PlayerName = _PlayerName;
        playerType = _playerType;
        playerNo = _playerNo;
    }

}
