using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSteps : MonoBehaviour
{
    public int no_player;
    public Stone[] playerStone;
    public int stepsRandom;

    public void MoveStone()
    {
        playerStone[no_player].MoveSteps(stepsRandom);
    }

}
