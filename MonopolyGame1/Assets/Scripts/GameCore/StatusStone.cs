using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusStone : MonoBehaviour
{
    public bool isWin;
    public Stone stone;
    public int stepsEffect;
    public bool isResistance;

    private void Start()
    {
        isResistance = false;
    }
    public void MoveStone(int _steps)
    {
        if (!isResistance)
        {
            Debug.Log("StatusStone-MoveStone : " + _steps);
            StartCoroutine(DelayMove(_steps));
            isResistance = true;
        }

    }

    IEnumerator DelayMove(int _steps)
    {
        yield return new WaitForSeconds(0.5f);
        stone.MoveSteps(_steps);
    }
    public void WinGame()
    {
        isWin = true;
        stone.currentRoute.gameControllerCenter.turnManagement.SetPlayerWin();
    }
}
