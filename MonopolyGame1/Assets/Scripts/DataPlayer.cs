using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer
{
    public string PlayerName;
    public TypeCharacter playerType;

    public DataPlayer(string _PlayerName, TypeCharacter _playerType)
    {
        PlayerName = _PlayerName;
        playerType = _playerType;
    }

}
