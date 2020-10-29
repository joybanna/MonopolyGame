using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusStone : MonoBehaviour
{
    public Stone stone;
    public int stepsEffect;

    public void MoveStone(int _steps)
    {
        Debug.Log("StatusStone-MoveStone : " + _steps);
        stone.MoveSteps(_steps);
    }
}
