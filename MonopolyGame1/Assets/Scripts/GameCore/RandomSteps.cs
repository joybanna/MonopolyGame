using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSteps : MonoBehaviour
{
    public GameControllerCenter gameControllerCenter;
    public int no_player;
    public Stone stone;
    public int stepsRandom;

    public void MoveStone()
    {
        stone.GetComponent<StatusStone>().isResistance = false;
        stone.MoveSteps(stepsRandom);
    }

}
