using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusStone : MonoBehaviour
{
    public bool isWin;
    public Stone stone;
    public int stepsEffect;

    public void MoveStone(int _steps)
    {
        Debug.Log("StatusStone-MoveStone : " + _steps);
        StartCoroutine(DelayMove(_steps));
    }

    IEnumerator DelayMove(int _steps)
    {
        yield return new WaitForSeconds(0.5f);
        stone.MoveSteps(_steps);
    }
}
